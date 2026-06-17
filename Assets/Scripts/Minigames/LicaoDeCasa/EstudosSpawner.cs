using System.Collections;
using UnityEngine;

public class EstudosSpawner : MonoBehaviour
{
    [Header("Prefabs e UI")]
    [SerializeField] private GameObject prefabBola;     // O prefab da bola clicker
    [SerializeField] private RectTransform painelArea;   // O painel UI limite onde as bolas nascem

    [Header("Configurações de Tempo")]
    [SerializeField] private float intervaloSpawn = 1.0f; // Tempo entre o nascimento de cada bola
    
    private bool minigameAtivo = false;
    private float cronometro = 0f;

    private void OnEnable()
    {
        minigameAtivo = true;
        cronometro = 0f;
    }

    private void OnDisable()
    {
        minigameAtivo = false;
    }

    void Update()
    {
        if (!minigameAtivo) return;

        cronometro += Time.deltaTime;

        if (cronometro >= intervaloSpawn)
        {
            cronometro = 0f;
            GerarBolaAleatoria();
        }
    }

    void GerarBolaAleatoria()
    {
        if (prefabBola == null || painelArea == null) return;

        // Instancia a nova bola dentro do container correto da sua UI
        GameObject novaBola = Instantiate(prefabBola, painelArea);
        RectTransform rectBola = novaBola.GetComponent<RectTransform>();

        // Reseta escala do prefab para garantir o tamanho original padrão
        rectBola.localScale = Vector3.one;

        // Garante ancoragem centralizada para o cálculo de posicionamento não quebrar
        rectBola.anchorMin = new Vector2(0.5f, 0.5f);
        rectBola.anchorMax = new Vector2(0.5f, 0.5f);
        rectBola.pivot = new Vector2(0.5f, 0.5f);

        // Calcula os limites reais de largura e altura baseados no tamanho do seu painel de estudos
        float limiteX = (painelArea.rect.width / 2f) - (rectBola.rect.width / 2f);
        float limiteY = (painelArea.rect.height / 2f) - (rectBola.rect.height / 2f);

        // Sorteia uma coordenada segura x e y dentro do retângulo
        float posXAleatoria = Random.Range(-limiteX, limiteX);
        float posYAleatoria = Random.Range(-limiteY, limiteY);

        // Aplica a posição sorteada na interface
        rectBola.anchoredPosition = new Vector2(posXAleatoria, posYAleatoria);
    }
}