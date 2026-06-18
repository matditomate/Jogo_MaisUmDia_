using UnityEngine;
using UnityEngine.EventSystems;

public class RitmoSpawner : MonoBehaviour
{
    [Header("Prefabs e UI")]
    [SerializeField] private GameObject prefabBolaRitmo; 
    [SerializeField] private RectTransform painelArea;     

    [Header("Configurações do Ritmo")]
    [SerializeField] private float tempoEntreBolas = 1.0f; 

    [Header("Regras de Fim de Jogo")]
    [SerializeField] private int pontosParaVencer = 30; // Jogo acaba quando atingir isso

    private bool jogoAtivo = false;
    private float cronometro = 0f;
    private int pontuacaoAtual = 0;

    private void OnEnable()
    {
        jogoAtivo = true;
        cronometro = 0f;
        pontuacaoAtual = 0;
    }

    private void OnDisable()
    {
        jogoAtivo = false;
    }

    void Update()
    {
        if (!jogoAtivo) return;

        cronometro += Time.deltaTime;

        if (cronometro >= tempoEntreBolas)
        {
            cronometro = 0f; 
            SpawnarNovaBola();
        }
    }

    void SpawnarNovaBola()
    {
        if (prefabBolaRitmo == null || painelArea == null) return;

        GameObject novaBola = Instantiate(prefabBolaRitmo, painelArea);
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

        // --- AQUI ESTÁ O TRUQUE ---
        // Pegamos o script RitmoBola da bola que acabou de nascer e nos inscrevemos no evento dela via código!
        RitmoSkillCheck scriptBola = novaBola.GetComponent<RitmoSkillCheck>();
        if (scriptBola != null)
        {
            scriptBola.aoComputarPontos.AddListener(AdicionarPontos);
        }
    }

    // Essa função será chamada automaticamente pela bolinha
    public void AdicionarPontos(int quantidade)
    {
        if (!jogoAtivo) return;

        pontuacaoAtual += quantidade;
        // Se quiser impedir que os pontos fiquem negativos:
        pontuacaoAtual = Mathf.Max(0, pontuacaoAtual); 

        Debug.Log($"Pontuação Atual: {pontuacaoAtual} / {pontosParaVencer}");

        // Checa a condição de vitória
        if (pontuacaoAtual >= pontosParaVencer)
        {
            FinalizarJogo();
        }
    }

    void FinalizarJogo()
    {
        jogoAtivo = false;
        Debug.Log("PARABÉNS! Você atingiu a pontuação e venceu o jogo de ritmo!");
        Robin.AlterarAnsiedade(-3);
        Robin.AlterarEnergia(-1);
        
        // Aqui você pode ativar uma tela de vitória, fechar o painel, etc.
    }

}