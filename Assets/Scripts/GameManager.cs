using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager = SOMENTE FUNÇÕES DE MANIPULAÇÃO, divididas em classes internas.
/// Lê e escreve os dados que estão no DataManager.
///
/// Uso:
///   GameManager.instance.statusRobin.AlterarSaciedade(2);
///   GameManager.instance.statusCasa.AlterarSujeira(-1);
///   GameManager.instance.statusCafe.SetLocked(true);
///   GameManager.instance.tempo.AvancarHoras(8);
///   GameManager.instance.som.TocarSFX(clip);
/// </summary>
[RequireComponent(typeof(DataManager))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const int STATUS_MIN = 0;
    private const int STATUS_MAX = 10;
    private const int MINUTOS_POR_DIA = 1440;

    // Disparado a cada minuto de jogo e sempre que o tempo é avançado manualmente
    // (antes era Casa.OnHorarioMudou)
    public static event Action OnHorarioMudou;

    // --- Módulos (classes internas) ---
    public Tempo tempo { get; private set; }
    public Audio som { get; private set; }
    public StatusRobin statusRobin { get; private set; }
    public StatusCasa statusCasa { get; private set; }
    public StatusCafe statusCafe { get; private set; }

    // ---------------------------------------------------------------------
    //  CICLO DE VIDA
    // ---------------------------------------------------------------------

    // Se a cena inicial não tiver o GameManager, cria um automaticamente
    // (evita NullReference ao dar Play numa cena qualquer no Editor).
    // Roda DEPOIS do Awake da cena, então não sobrescreve o que está configurado no Inspector.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void GarantirInstancia()
    {
        if (instance != null) return;

        Debug.LogWarning("GameManager não encontrado na cena inicial. Criando um automaticamente (sem clipes de áudio atribuídos).");
        new GameObject("Managers").AddComponent<GameManager>(); // RequireComponent adiciona o DataManager
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        DataManager dados = GetComponent<DataManager>();

        tempo = new Tempo(dados);
        som = new Audio(dados, gameObject);
        statusRobin = new StatusRobin(dados);
        statusCasa = new StatusCasa(dados);
        statusCafe = new StatusCafe(dados);

        som.Inicializar();
        tempo.AtualizarRelogio(false);

        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= AoCarregarCena;
            instance = null;
        }
    }

    private void Update()
    {
        // Ignora o duplicado que está prestes a ser destruído
        if (instance != this) return;

        tempo.Atualizar();
    }

    private void AoCarregarCena(Scene scene, LoadSceneMode mode)
    {
        som.AoCarregarCena();
    }

    // ---------------------------------------------------------------------
    //  UTILIDADES COMPARTILHADAS
    // ---------------------------------------------------------------------

    // Soma o valor e mantém o resultado entre 0 e 10
    private static int Limitar(int atual, int valor)
    {
        return Mathf.Clamp(atual + valor, STATUS_MIN, STATUS_MAX);
    }

    private static void NotificarHorarioMudou()
    {
        OnHorarioMudou?.Invoke();
    }

    // =====================================================================
    //  CLASSE: TEMPO  (origem: Tempo.cs + horário/dia do Casa.cs)
    //  Fonte única do relógio: DataManager.totalMinutosDoJogo
    // =====================================================================
    public class Tempo
    {
        private readonly DataManager d;

        internal Tempo(DataManager dados)
        {
            d = dados;
        }

        // --- Leitura (derivados de totalMinutosDoJogo) ---
        public float Horas => (d.totalMinutosDoJogo % MINUTOS_POR_DIA) / 60f; // 0 a 24 (antes: Casa.horario)
        public int Dia => d.totalMinutosDoJogo / MINUTOS_POR_DIA + 1;         // começa em 1 (antes: Casa.dia)
        public string RelogioTexto => d.relogioTexto;
        public bool Ativo => d.tempoAtivo;

        // Chamado pelo Update do GameManager
        internal void Atualizar()
        {
            if (!d.tempoAtivo) return;

            // A ansiedade pode acelerar o tempo (liga/desliga em DataManager.ansiedadeAceleraTempo)
            float escala = d.ansiedadeAceleraTempo
                ? CalcularVelocidadeDoTempo(d.escalaDeTempo, d.ansiedadeRobin)
                : d.escalaDeTempo;

            // Acumula segundos reais x escala
            d.minutosFlutuantes += Time.deltaTime * escala;

            // Converte o acumulado em minutos inteiros (não perde tempo se a escala for alta)
            int minutosInteiros = Mathf.FloorToInt(d.minutosFlutuantes);
            if (minutosInteiros >= 1)
            {
                d.minutosFlutuantes -= minutosInteiros;
                AvancarMinutos(minutosInteiros);
            }
        }

        // Avança o relógio (ex.: dormir, tomar banho). Substitui Casa.AlterarHorario.
        public void AvancarMinutos(int minutos)
        {
            if (minutos <= 0) return;

            d.totalMinutosDoJogo += minutos;
            AtualizarRelogio(true);
        }

        public void AvancarHoras(float horas)
        {
            AvancarMinutos(Mathf.RoundToInt(horas * 60f));
        }

        // Define a hora do dia ATUAL (ex.: 18.5f = 18:30), sem mudar o dia.
        // Substitui GameManager.SetHorario. ATENÇÃO: confirme se o SetHorario antigo
        // também avançava o dia quando a hora nova era menor que a atual.
        public void DefinirHorario(float horas)
        {
            int inicioDoDia = (d.totalMinutosDoJogo / MINUTOS_POR_DIA) * MINUTOS_POR_DIA;
            int minutosDoDia = Mathf.Clamp(Mathf.RoundToInt(horas * 60f), 0, MINUTOS_POR_DIA - 1);

            d.totalMinutosDoJogo = inicioDoDia + minutosDoDia;
            d.minutosFlutuantes = 0f;
            AtualizarRelogio(true);
        }

        internal void AtualizarRelogio(bool notificar)
        {
            d.relogioTexto = CalcularRelogio(d.totalMinutosDoJogo);

            if (notificar)
                NotificarHorarioMudou();
        }

        public string CalcularRelogio(int totalMinutosAcumulados)
        {
            int dias = totalMinutosAcumulados / MINUTOS_POR_DIA + 1;               // Dia 1 = primeiro dia
            int horas = (totalMinutosAcumulados % MINUTOS_POR_DIA) / 60;
            int minutos = totalMinutosAcumulados % 60;

            return $"Dia {dias}\n{horas:D2}:{minutos:D2}";
        }

        // Ansiedade 0 = x1 (normal) | Ansiedade 10 = x2 (dobro)
        // Para acelerar ainda mais, aumente o divisor (ex.: ansiedadeAtual / 5f => x3)
        public float CalcularVelocidadeDoTempo(float velocidadeBase, float ansiedadeAtual)
        {
            float modificadorEmocional = 1f + (ansiedadeAtual / 10f);
            return velocidadeBase * modificadorEmocional;
        }

        public void Pausar() => d.tempoAtivo = false;
        public void Retomar() => d.tempoAtivo = true;
    }

    // =====================================================================
    //  CLASSE: AUDIO  (origem: AudioManager.cs)
    // =====================================================================
    public class Audio
    {
        private readonly DataManager d;
        private readonly GameObject hospedeiro;

        internal Audio(DataManager dados, GameObject hospedeiro)
        {
            d = dados;
            this.hospedeiro = hospedeiro;
        }

        // Cria os AudioSources se não foram atribuídos no Inspector
        internal void Inicializar()
        {
            if (d.sfxSource == null)
            {
                d.sfxSource = hospedeiro.AddComponent<AudioSource>();
                d.sfxSource.playOnAwake = false;
            }

            if (d.loopSource == null)
            {
                d.loopSource = hospedeiro.AddComponent<AudioSource>();
                d.loopSource.playOnAwake = false;
                d.loopSource.loop = true;
            }
        }

        public void AgendarSomDeFechar()
        {
            d.deveTocarFecharNaProximaCena = true;
        }

        internal void AoCarregarCena()
        {
            if (d.deveTocarFecharNaProximaCena)
            {
                TocarSFX(d.somPortaFechando);
                d.deveTocarFecharNaProximaCena = false;
            }
        }

        public void TocarSFX(AudioClip som)
        {
            if (som != null && d.sfxSource != null)
            {
                d.sfxSource.PlayOneShot(som);
            }
        }

        public void TocarLooping(AudioClip som)
        {
            if (som != null && d.loopSource != null)
            {
                if (d.loopSource.clip == som && d.loopSource.isPlaying) return;

                d.loopSource.clip = som;
                d.loopSource.loop = true;
                d.loopSource.Play();
            }
        }

        public void PararLooping()
        {
            if (d.loopSource != null && d.loopSource.isPlaying)
            {
                d.loopSource.Stop();
                d.loopSource.clip = null;
            }
        }
    }

    // =====================================================================
    //  CLASSE: STATUS ROBIN  (origem: Robin.cs)
    // =====================================================================
    public class StatusRobin
    {
        private readonly DataManager d;

        internal StatusRobin(DataManager dados)
        {
            d = dados;
        }

        // Positivo = mais saciada | Negativo = mais faminta
        // (antes: AlterarFome, com sinal invertido - comer era valor NEGATIVO)
        public void AlterarSaciedade(int valor)
        {
            d.saciedadeRobin = Limitar(d.saciedadeRobin, valor);
            Debug.Log($"Saciedade de Robin: {d.saciedadeRobin}");
        }

        public void AlterarHigiene(int valor)
        {
            d.higieneRobin = Limitar(d.higieneRobin, valor);
            Debug.Log($"Higiene de Robin: {d.higieneRobin}");
        }

        public void AlterarDiversao(int valor)
        {
            d.diversaoRobin = Limitar(d.diversaoRobin, valor);
            Debug.Log($"Diversão de Robin: {d.diversaoRobin}");
        }

        public void AlterarEnergia(int valor)
        {
            d.energiaRobin = Limitar(d.energiaRobin, valor);
            Debug.Log($"Energia de Robin: {d.energiaRobin}");
        }

        // Atenção: ansiedade é a exceção (0 = calma, 10 = crise)
        public void AlterarAnsiedade(int valor)
        {
            d.ansiedadeRobin = Limitar(d.ansiedadeRobin, valor);
            Debug.Log($"Ansiedade de Robin: {d.ansiedadeRobin}");

            AtivarEfeitoAnsiedade();

            if (d.ansiedadeRobin == STATUS_MAX)
            {
                AnsiedadeAnim componentesAnsiedade = FindFirstObjectByType<AnsiedadeAnim>();
                if (componentesAnsiedade != null)
                {
                    componentesAnsiedade.AtualizarEstadoAnsiedade();
                }
            }
        }

        public void AlterarProgresso(int valor)
        {
            d.progressoRobin = Limitar(d.progressoRobin, valor);
            Debug.Log($"Progresso de Robin: {d.progressoRobin}");
        }

        // TODO: COLE AQUI o corpo do seu AtivarEfeitoAnsiedade() antigo
        // (ele existia no GameManager anterior, que eu não tive acesso).
        public void AtivarEfeitoAnsiedade()
        {
        }
    }

    // =====================================================================
    //  CLASSE: STATUS CASA  (origem: Casa.cs)
    //  Horário/dia agora ficam em Tempo.
    // =====================================================================
    public class StatusCasa
    {
        private readonly DataManager d;

        internal StatusCasa(DataManager dados)
        {
            d = dados;
        }

        public void AlterarSujeira(int valor)
        {
            d.sujeiraCasa = Limitar(d.sujeiraCasa, valor);
            Debug.Log($"Sujeira da casa: {d.sujeiraCasa}");
        }

        public void AlterarAguaPlanta(int valor)
        {
            d.aguaPlanta = Limitar(d.aguaPlanta, valor);
            Debug.Log($"Água das plantas: {d.aguaPlanta}");
        }
    }

    // =====================================================================
    //  CLASSE: STATUS CAFÉ  (origem: Cafe.cs - apenas a parte de status)
    // =====================================================================
    public class StatusCafe
    {
        private readonly DataManager d;

        internal StatusCafe(DataManager dados)
        {
            d = dados;
        }

        public bool Locked => d.cafeLocked;

        // Positivo = mais saciado | Negativo = mais faminto
        // (antes: AlterarFome, com sinal invertido)
        public void AlterarSaciedade(int valor)
        {
            d.saciedadeCafe = Limitar(d.saciedadeCafe, valor);
            Debug.Log("Saciedade do Café agora é: " + d.saciedadeCafe);
        }

        public void AlterarAtencao(int valor)
        {
            d.atencaoCafe = Limitar(d.atencaoCafe, valor);
            Debug.Log("Atenção do Café agora é: " + d.atencaoCafe);
        }

        public void SetLocked(bool estado)
        {
            d.cafeLocked = estado;
            Debug.Log("Estado Locked do Café: " + d.cafeLocked);
        }
    }
}