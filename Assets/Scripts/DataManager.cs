using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// DataManager = SOMENTE VARIÁVEIS (estado do jogo + referências de áudio).
// Persiste entre cenas (DontDestroyOnLoad) e mostra os valores ao vivo no Inspector.
// Toda a lógica/manipulação fica no GameManager.
//
// Campos de INSTÂNCIA (não static): o Unity não serializa static,
// então [Header] e [Range] não apareceriam no Inspector.
//
// PADRÃO DOS STATUS: 10 = bom, 0 = ruim.
// ÚNICA EXCEÇÃO: ansiedadeRobin (0 = calma, 10 = crise).

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ROBIN  (origem: Robin.cs)
    [Header("ROBIN - Status (10 = bom, 0 = ruim)")]
    [Range(0, 10)] public int saciedadeRobin = 6;   // 10 = Saciada, 0 = Faminta (antes: fome = 4)
    [Range(0, 10)] public int higieneRobin = 6;     // 10 = Limpo, 0 = Imundo
    [Range(0, 10)] public int diversaoRobin = 7;    // 10 = Feliz, 0 = Entediado
    [Range(0, 10)] public int energiaRobin = 8;     // 10 = Disposto, 0 = Exausto

    [Header("ROBIN - Status especiais")]
    [Range(0, 10)] public int ansiedadeRobin = 6;   // EXCEÇÃO: 0 = Calma, 10 = Crise
    [Range(0, 10)] public int progressoRobin = 0;   // 0 = Início, 10 = Meta

    // CASA  (origem: Casa.cs)
    [Header("CASA - Status")]
    [Range(0, 10)] public int sujeiraCasa = 5;      // 10 = muito suja (quanto maior, pior)
    [Range(0, 10)] public int aguaPlanta = 6;       // 10 = bem regada

    // OBS: horário e dia da casa NÃO existem mais aqui.
    // Agora são derivados de totalMinutosDoJogo (fonte única, ver TEMPO).

    // CAFÉ  (origem: Cafe.cs)
    [Header("CAFÉ - Status (10 = bom, 0 = ruim)")]
    [Range(0, 10)] public int saciedadeCafe = 5;    // 10 = Saciado, 0 = Faminto (antes: fome = 5)
    [Range(0, 10)] public int atencaoCafe = 5;

    [Header("CAFÉ - Controle")]
    public bool cafeLocked = false;                 // Trava total de interação

    // TEMPO  (origem: Tempo.cs + horário/dia do Casa.cs)
    [Header("TEMPO - Configuração")]
    public float escalaDeTempo = 1f;                // 1 segundo real = 1 minuto no jogo
    public bool tempoAtivo = true;
    [Tooltip("Se ligado, a ansiedade de Robin acelera o tempo (ansiedade 10 = tempo x2).")]
    public bool ansiedadeAceleraTempo = true;

    [Header("TEMPO - Estado (fonte única do relógio)")]
    public int totalMinutosDoJogo = 360;            // 360 = Dia 1, 06:00 (mesmo início do antigo Casa.horario)
    public float minutosFlutuantes = 0f;            // Acumulador interno (fração de minuto)
    public string relogioTexto = "Dia 1\n06:00";    // Texto pronto para a UI ler

    // ÁUDIO  (origem: AudioManager.cs)
    [Header("ÁUDIO - Componentes (criados automaticamente se vazios)")]
    public AudioSource sfxSource;
    public AudioSource loopSource;

    [Header("ÁUDIO - Sons Globais de Transição")]
    public AudioClip somPortaFechando;

    [Header("ÁUDIO - Estado")]
    public bool deveTocarFecharNaProximaCena = false;

    [Header("NAVEGAÇÃO")]
    public string portaDeDestino;

    // TUTORIAIS
    [Header("TUTORIAIS")]
    public HashSet<string> tutoriaisVistos = new HashSet<string>();
}