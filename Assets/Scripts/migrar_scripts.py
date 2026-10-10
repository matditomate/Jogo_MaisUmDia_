#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
migrar_scripts.py - migra chamadas antigas para o GameManager/DataManager novos.

Uso:
    python migrar_scripts.py "C:\\caminho\\Projeto\\Assets\\Scripts"            (SIMULAÇÃO, não altera nada)
    python migrar_scripts.py "C:\\caminho\\Projeto\\Assets\\Scripts" --apply    (aplica, com backup)

O backup (cópia original dos arquivos alterados) vai para a pasta "backup_migracao",
na RAIZ do projeto (fora de Assets, para o Unity não compilar as cópias).

O que ele faz sozinho:
  - Robin.AlterarX(...)               -> GameManager.instance.statusRobin.AlterarX(...)
  - Robin.AlterarFome(n)              -> ...statusRobin.AlterarSaciedade(-n)   (sinal invertido)
  - Robin.higiene/energia/... (leitura) -> DataManager.instance.higieneRobin/energiaRobin/...
  - Robin.fome (leitura)              -> (10 - DataManager.instance.saciedadeRobin)
  - Casa.*, Cafe.* (estáticos), AudioManager.instance.*, PausarTempo()/RetomarTempo(),
    variáveis dos tipos antigos (Casa casa; Tempo t; ...)
  - Membros do GameManager ANTIGO: SetHorario(x) -> tempo.DefinirHorario(x), horario/dia -> tempo.Horas/Dia,
    AlterarHorario(x) -> tempo.AvancarHoras(x), AlterarAguaPlanta/AlterarSujeira -> statusCasa.*
  - Remove campos "Robin robin;", "Casa casa;", "Tempo tempo;", "AudioManager audioManager;"
O que ele NÃO faz (aparece no relatório como REVISAR): atribuições (Robin.x = ..., Casa.dia += 1),
FindFirstObjectByType<Casa>(), campos com inicializador e qualquer membro do GameManager antigo que
não exista no novo (ex.: portaDeDestino) - esses ficam listados no fim do relatório.
"""
import argparse
import re
import shutil
import sys
from pathlib import Path

PULAR = {"DataManager.cs", "GameManager.cs", "Cafe.cs", "ClickDetector.cs"}

SG = "GameManager.instance"
DM = "DataManager.instance"

# Atribuição logo depois de um membro (=, +=, -=, *=, /=, ++, --) mas não "=="
ATRIB = r"\s*(?:=(?!=)|[+\-*/]=|\+\+|--)"
NAO_ATRIB = rf"(?!{ATRIB})"

LEITURAS_ROBIN = {
    "higiene": f"{DM}.higieneRobin",
    "diversao": f"{DM}.diversaoRobin",
    "energia": f"{DM}.energiaRobin",
    "ansiedade": f"{DM}.ansiedadeRobin",
    "progresso": f"{DM}.progressoRobin",
    "fome": f"(10 - {DM}.saciedadeRobin)",
}

LEITURAS_CASA = {
    "sujeira": f"{DM}.sujeiraCasa",
    "aguaPlanta": f"{DM}.aguaPlanta",
    "horario": f"{SG}.tempo.Horas",
    "dia": f"{SG}.tempo.Dia",
}

LEITURAS_CAFE = {
    "atencao": f"{DM}.atencaoCafe",
    "locked": f"{DM}.cafeLocked",
    "fome": f"(10 - {DM}.saciedadeCafe)",
}

DECL_LINHA = re.compile(
    r"^[ \t]*(?:\[[^\]\n]*\][ \t]*)*(?:(?:public|private|protected|internal|static)[ \t]+)*"
    r"(?:Robin|Casa|Tempo|AudioManager)[ \t]+\w+[ \t]*;[ \t]*(?://[^\n]*)?\r?\n",
    re.M,
)

# Membros do GameManager que NÃO existem no novo (vieram do GameManager antigo)
GM_DESCONHECIDOS = re.compile(
    r"\bGameManager\.(?:instance\.)?(?!instance\b|OnHorarioMudou\b|Tempo\b|Audio\b|StatusRobin\b|StatusCasa\b|StatusCafe\b|tempo\b|som\b|statusRobin\b|statusCasa\b|statusCafe\b)(\w+)"
)

SOBRAS = re.compile(
    r"\b(?:Robin|Casa|Tempo|AudioManager)\b"
    r"|\bCafe\.(?:fome|atencao|locked|AlterarFome|AlterarAtencao|SetLocked)\b"
    r"|" + GM_DESCONHECIDOS.pattern
)


def inverter_sinal(arg):
    a = arg.strip()
    m = re.fullmatch(r"-\s*(\d+(?:\.\d+)?f?)", a)
    if m:
        return m.group(1)
    m = re.fullmatch(r"-\s*([A-Za-z_]\w*)", a)
    if m:
        return m.group(1)
    m = re.fullmatch(r"\+?\s*(\d+(?:\.\d+)?f?)", a)
    if m:
        return "-" + m.group(1)
    return f"-({a})"


def sub(texto, regex, repl, contagem, chave):
    novo, n = re.subn(regex, repl, texto)
    if n:
        contagem[chave] = contagem.get(chave, 0) + n
    return novo


def nomes_declarados(texto, tipo):
    return set(re.findall(rf"\b{tipo}\s+(\w+)\s*(?:;|=|,|\))", texto))


def migrar_texto(texto):
    contagem = {}
    t = texto

    # --- 1) Variáveis dos tipos antigos (casa.AlterarSujeira(...), tempo.PausarTempo(), ...) ---
    for v in nomes_declarados(t, "Casa"):
        ev = re.escape(v)
        t = sub(t, rf"(?<![\w.]){ev}\.AlterarSujeira\(", lambda m: f"{SG}.statusCasa.AlterarSujeira(", contagem, "Casa (variável)")
        t = sub(t, rf"(?<![\w.]){ev}\.AlterarAguaPlanta\(", lambda m: f"{SG}.statusCasa.AlterarAguaPlanta(", contagem, "Casa (variável)")
        t = sub(t, rf"(?<![\w.]){ev}\.AlterarHorario\(", lambda m: f"{SG}.tempo.AvancarHoras(", contagem, "Casa (variável)")

    for v in nomes_declarados(t, "Tempo"):
        ev = re.escape(v)
        t = sub(t, rf"(?<![\w.]){ev}\.PausarTempo\(", lambda m: f"{SG}.tempo.Pausar(", contagem, "Tempo (variável)")
        t = sub(t, rf"(?<![\w.]){ev}\.RetomarTempo\(", lambda m: f"{SG}.tempo.Retomar(", contagem, "Tempo (variável)")
        t = sub(t, rf"(?<![\w.]){ev}\.(CalcularRelogio|CalcularVelocidadeDoTempo)\(", lambda m: f"{SG}.tempo.{m.group(1)}(", contagem, "Tempo (variável)")
        t = sub(t, rf"(?<![\w.]){ev}\.(totalMinutosDoJogo|tempoAtivo)\b", lambda m: f"{DM}.{m.group(1)}", contagem, "Tempo (variável)")

    for v in nomes_declarados(t, "AudioManager"):
        ev = re.escape(v)
        t = sub(t, rf"(?<![\w.]){ev}\.(TocarSFX|TocarLooping|PararLooping|AgendarSomDeFechar)\(", lambda m: f"{SG}.som.{m.group(1)}(", contagem, "AudioManager (variável)")

    # --- 2) Robin ---
    t = sub(t, r"\bRobin\.AlterarFome\(([^()]*)\)", lambda m: f"{SG}.statusRobin.AlterarSaciedade({inverter_sinal(m.group(1))})", contagem, "Robin.AlterarFome -> AlterarSaciedade (sinal invertido)")
    t = sub(t, r"\bRobin\.(AlterarEnergia|AlterarHigiene|AlterarDiversao|AlterarAnsiedade|AlterarProgresso)\(", lambda m: f"{SG}.statusRobin.{m.group(1)}(", contagem, "Robin.AlterarX")
    for campo, destino in LEITURAS_ROBIN.items():
        t = sub(t, rf"\bRobin\.{campo}\b{NAO_ATRIB}", lambda m, d=destino: d, contagem, "Robin.<campo> (leitura)")

    # --- 3) Casa (estático / instancia) ---
    t = sub(t, r"\bCasa\.OnHorarioMudou\b", lambda m: "GameManager.OnHorarioMudou", contagem, "Casa.OnHorarioMudou")
    t = sub(t, r"\bCasa\.AlterarAguaPlanta\(", lambda m: f"{SG}.statusCasa.AlterarAguaPlanta(", contagem, "Casa.AlterarAguaPlanta")
    t = sub(t, r"\bCasa\.(?:instancia|instance)\.AlterarSujeira\(", lambda m: f"{SG}.statusCasa.AlterarSujeira(", contagem, "Casa.instancia.AlterarSujeira")
    t = sub(t, r"\bCasa\.(?:instancia|instance)\.AlterarHorario\(", lambda m: f"{SG}.tempo.AvancarHoras(", contagem, "Casa.instancia.AlterarHorario")
    for campo, destino in LEITURAS_CASA.items():
        t = sub(t, rf"\bCasa\.{campo}\b{NAO_ATRIB}", lambda m, d=destino: d, contagem, "Casa.<campo> (leitura)")

    # --- 4) Cafe (membros estáticos antigos) ---
    t = sub(t, r"\bCafe\.AlterarFome\(([^()]*)\)", lambda m: f"{SG}.statusCafe.AlterarSaciedade({inverter_sinal(m.group(1))})", contagem, "Cafe.AlterarFome -> AlterarSaciedade (sinal invertido)")
    t = sub(t, r"\bCafe\.AlterarAtencao\(", lambda m: f"{SG}.statusCafe.AlterarAtencao(", contagem, "Cafe.AlterarAtencao")
    t = sub(t, r"\bCafe\.SetLocked\(", lambda m: f"{SG}.statusCafe.SetLocked(", contagem, "Cafe.SetLocked")
    t = sub(t, r"\bCafe\.locked\s*=(?!=)\s*([^;]+);", lambda m: f"{SG}.statusCafe.SetLocked({m.group(1).strip()});", contagem, "Cafe.locked = x -> SetLocked(x)")
    for campo, destino in LEITURAS_CAFE.items():
        t = sub(t, rf"\bCafe\.{campo}\b{NAO_ATRIB}", lambda m, d=destino: d, contagem, "Cafe.<campo> (leitura)")

    # --- 5) AudioManager (singleton antigo) ---
    t = sub(t, r"\bAudioManager\.instance\.", lambda m: f"{SG}.som.", contagem, "AudioManager.instance.X")
    t = sub(t, r"\bAudioManager\.instance\b", lambda m: SG, contagem, "AudioManager.instance (sozinho) - REVISAR")

    # --- 6) GameManager antigo ---
    t = sub(t, r"\bGameManager\.instance\.AlterarHorario\(", lambda m: f"{SG}.tempo.AvancarHoras(", contagem, "GameManager.instance.AlterarHorario")
    t = sub(t, r"\bGameManager\.instance\.AtivarEfeitoAnsiedade\(", lambda m: f"{SG}.statusRobin.AtivarEfeitoAnsiedade(", contagem, "GameManager.instance.AtivarEfeitoAnsiedade")
    t = sub(t, r"\bGameManager\.(?:instance\.)?SetHorario\(", lambda m: f"{SG}.tempo.DefinirHorario(", contagem, "GameManager.SetHorario -> tempo.DefinirHorario (VERIFICAR semantica)")
    t = sub(t, r"\bGameManager\.(?:instance\.)?AlterarAguaPlanta\(", lambda m: f"{SG}.statusCasa.AlterarAguaPlanta(", contagem, "GameManager.AlterarAguaPlanta")
    t = sub(t, r"\bGameManager\.(?:instance\.)?AlterarSujeira\(", lambda m: f"{SG}.statusCasa.AlterarSujeira(", contagem, "GameManager.AlterarSujeira")
    t = sub(t, r"\bGameManager\.(?:instance\.)?horario\b" + NAO_ATRIB, lambda m: f"{SG}.tempo.Horas", contagem, "GameManager.horario (leitura) -> tempo.Horas")
    t = sub(t, r"\bGameManager\.(?:instance\.)?dia\b" + NAO_ATRIB, lambda m: f"{SG}.tempo.Dia", contagem, "GameManager.dia (leitura) -> tempo.Dia")
    t = sub(t, r"[\w.]+\.AlterarHorario\(", lambda m: f"{SG}.tempo.AvancarHoras(", contagem, "X.AlterarHorario -> tempo.AvancarHoras (VERIFICAR unidade: horas)")
    t = sub(t, r"\b\w+\.PausarTempo\(\)", lambda m: f"{SG}.tempo.Pausar()", contagem, "PausarTempo()")
    t = sub(t, r"\b\w+\.RetomarTempo\(\)", lambda m: f"{SG}.tempo.Retomar()", contagem, "RetomarTempo()")

    # --- 7) Remove campos dos tipos antigos (sem inicializador) ---
    t = sub(t, DECL_LINHA, "", contagem, "campos removidos (Robin/Casa/Tempo/AudioManager)")

    return t, contagem


def sem_strings_e_comentarios(linha):
    linha = re.sub(r'"(?:\\.|[^"\\])*"', '""', linha)
    return linha.split("//", 1)[0]


def sobras(texto):
    achados = []
    for i, linha in enumerate(texto.split("\n"), 1):
        limpa = sem_strings_e_comentarios(linha.rstrip("\r"))
        if SOBRAS.search(limpa):
            achados.append((i, linha.strip()))
    return achados


def raiz_backup(pasta):
    for p in [pasta, *pasta.parents]:
        if p.name == "Assets":
            return p.parent / "backup_migracao"
    return pasta.parent / "backup_migracao"


def main():
    ap = argparse.ArgumentParser(description="Migra chamadas antigas para GameManager/DataManager.")
    ap.add_argument("pasta", help="Pasta com os scripts (ex.: Assets/Scripts)")
    ap.add_argument("--apply", action="store_true", help="Aplica as mudanças (com backup). Sem isso, só simula.")
    args = ap.parse_args()

    pasta = Path(args.pasta).resolve()
    if not pasta.is_dir():
        print(f"Pasta não encontrada: {pasta}")
        sys.exit(1)

    destino_backup = raiz_backup(pasta)
    total_alterados = 0
    total_revisar = 0
    desconhecidos = {}   # membro do GameManager antigo -> lista de "arquivo:linha"

    for arq in sorted(pasta.rglob("*.cs")):
        if arq.name in PULAR:
            continue

        bruto = arq.read_bytes()
        bom = bruto.startswith(b"\xef\xbb\xbf")
        try:
            original = bruto.decode("utf-8-sig")
        except UnicodeDecodeError:
            print(f"[PULADO] {arq.relative_to(pasta)}: não é UTF-8")
            continue

        novo, contagem = migrar_texto(original)
        pendentes = sobras(novo)

        if not contagem and not pendentes:
            continue

        rel = arq.relative_to(pasta)
        print(f"\n=== {rel}")
        for chave, n in contagem.items():
            print(f"   [auto] {n}x  {chave}")
        for linha, trecho in pendentes:
            print(f"   [REVISAR] linha {linha}: {trecho}")
        total_revisar += len(pendentes)
        for i, linha_txt in enumerate(novo.split("\n"), 1):
            for nome in GM_DESCONHECIDOS.findall(sem_strings_e_comentarios(linha_txt.rstrip("\r"))):
                desconhecidos.setdefault(nome, []).append(f"{rel}:{i}")

        if novo != original:
            total_alterados += 1
            if args.apply:
                dst = destino_backup / rel
                dst.parent.mkdir(parents=True, exist_ok=True)
                shutil.copy2(arq, dst)
                arq.write_bytes((b"\xef\xbb\xbf" if bom else b"") + novo.encode("utf-8"))

    print("\n" + "-" * 60)
    modo = "APLICADO" if args.apply else "SIMULAÇÃO (nada foi alterado)"
    print(f"{modo}: {total_alterados} arquivo(s) com mudanças automáticas, {total_revisar} linha(s) para REVISAR.")
    if desconhecidos:
        print("\nMEMBROS DO GAMEMANAGER ANTIGO SEM EQUIVALENTE NO NOVO (preciso do GameManager antigo para migrar):")
        for nome, locais in sorted(desconhecidos.items()):
            print(f"   - {nome}: {len(locais)}x  (ex.: {locais[0]})")
    if args.apply and total_alterados:
        print(f"Backup dos originais em: {destino_backup}")
    if not args.apply:
        print("Rode de novo com --apply para aplicar.")


if __name__ == "__main__":
    main()
