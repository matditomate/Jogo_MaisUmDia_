using UnityEngine;

public class EstudosSpawner : MonoBehaviour
{
    [Header("Prefabs e UI")]
    [SerializeField] private GameObject prefabBola;     
    [SerializeField] private RectTransform painelArea;   // Área onde as bolas podem nascer

    [Header("Configurações do Minijogo")]
    [SerializeField] private float intervaloSpawn = 1.2f; // Tempo para nascer nova bola
    [SerializeField] private int limiteBolasDerrota = 10; // Se acumular 10 bolas, perde
    [SerializeField] private float tempoParaVencer = 10f; // Segundos com tela limpa para ganhar
    
    [Header("Referências")]
    [SerializeField] private TriggerLicaoDeCasa scriptTrigger; // Para fechar o painel

    private bool minigameAtivo = false;
    private float cronometroSpawn = 0f;
    private float cronometroVitoria = 0f;
    private bool jaSpawnouPrimeiraBola = false; // Evita que o jogador vença no segundo 0

    public Robin robin;

    private void OnEnable()
    {
        minigameAtivo = true;
        cronometroSpawn = 0f;
        // cronometroVitoria = 0f;
        jaSpawnouPrimeiraBola = false;

        // Limpa a tela toda vez que o minijogo é aberto
        if (painelArea != null)
        {
            foreach (Estudos bolaAntiga in painelArea.GetComponentsInChildren<Estudos>(true))
            {
                Destroy(bolaAntiga.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        minigameAtivo = false;
    }

    void Update()
    {
        if (!minigameAtivo) return;

        cronometroSpawn += Time.deltaTime;

        // 1. GERAÇÃO DE BOLAS
        if (cronometroSpawn >= intervaloSpawn)
        {
            cronometroSpawn = 0f;
            GerarBolaAleatoria();
            jaSpawnouPrimeiraBola = true;
        }

        // 2. CONTAGEM REAL DE BOLAS NA TELA
        int totalDeBolasNaTela = painelArea.GetComponentsInChildren<Estudos>().Length;

        // 3. CONDIÇÃO DE DERROTA (Tela Cheia)
        if (totalDeBolasNaTela >= limiteBolasDerrota)
        {
            FinalizarMinigame(false); // Falso = Perdeu
            return;
        }

        // 4. CONDIÇÃO DE VITÓRIA (Tela Limpa por 10s)
        if (jaSpawnouPrimeiraBola && totalDeBolasNaTela == 0)
        {
            cronometroVitoria += Time.deltaTime;
            Debug.Log(cronometroVitoria);
            
            if (cronometroVitoria >= tempoParaVencer)
            {
                FinalizarMinigame(true); // Verdadeiro = Ganhou
            }
        }
    }

    void GerarBolaAleatoria()
    {
        if (prefabBola == null || painelArea == null) return;

        GameObject novaBola = Instantiate(prefabBola, painelArea);
        RectTransform rectBola = novaBola.GetComponent<RectTransform>();

        rectBola.localScale = Vector3.one;
        rectBola.anchorMin = new Vector2(0.5f, 0.5f);
        rectBola.anchorMax = new Vector2(0.5f, 0.5f);
        rectBola.pivot = new Vector2(0.5f, 0.5f);

        float margem = 60f;
        float limiteX = Mathf.Max(0, (painelArea.rect.width / 2f) - margem);
        float limiteY = Mathf.Max(0, (painelArea.rect.height / 2f) - margem);

        float posX = Random.Range(-limiteX, limiteX);
        float posY = Random.Range(-limiteY, limiteY);

        rectBola.anchoredPosition = new Vector2(posX, posY);
    }

    void FinalizarMinigame(bool venceu)
    {
        minigameAtivo = false;

        if (venceu)
        {
            Debug.Log("VITÓRIA! O jogador manteve o foco e limpou as distrações!");
            TriggerLicaoDeCasa.minigameBloqueado = true; 
            Robin.AlterarEnergia(-3);
            Robin.AlterarAnsiedade(-2);
            Robin.AlterarProgresso(3);
        }
        else
        {
            Debug.Log("DERROTA! As distrações preencheram a tela toda!");
        }

        if (scriptTrigger != null)
        {
            scriptTrigger.FecharPanel();
        }
    }
}