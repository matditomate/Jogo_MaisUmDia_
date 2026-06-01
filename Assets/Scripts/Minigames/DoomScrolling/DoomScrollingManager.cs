using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DoomScrollingManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Configurações do Feed")]
    [SerializeField] private Sprite[] listaDeReels;  // Coloque suas imagens aqui no Inspector
    
    [Header("Configuração de Movimento")]
    [Tooltip("Distância em pixels que o mouse precisa arrastar para cima para passar de página")]
    [SerializeField] private float distanciaMinimaArrasto = 60f; 

    private Image uiImageComponent;
    private Vector2 posicaoInicialClique;

    void Awake()
    {
        // Pega automaticamente o componente Image do próprio objeto
        uiImageComponent = GetComponent<Image>();
    }

    void OnEnable()
    {
        // Toda vez que abrir o celular, mostra um post aleatório inicial
        MostrarReelAleatorio();
    }

    // Detecta o momento exato do clique
    public void OnBeginDrag(PointerEventData eventData)
    {
        posicaoInicialClique = eventData.position;
    }

    // Obrigatório para a Unity registrar que o objeto está sendo arrastado
    public void OnDrag(PointerEventData eventData) { }

    // Detecta quando o jogador solta o clique do mouse
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 posicaoFinalClique = eventData.position;
        
        // Calcula a distância vertical do arrasto (Final - Inicial)
        float diferencaY = posicaoFinalClique.y - posicaoInicialClique.y;

        // Se arrastou para cima (positivo) e passou da distância mínima
        if (diferencaY > distanciaMinimaArrasto)
        {
            AvançarFeed();
        }
    }

    private void AvançarFeed()
    {
        MostrarReelAleatorio();
        AdicionarTempo(5); // Avisa ao jogo que se passaram 5 minutos
    }

    private void MostrarReelAleatorio()
    {
        if (listaDeReels == null || listaDeReels.Length == 0)
        {
            Debug.LogWarning("Nenhuma imagem foi colocada na lista de Reels do ImageReel!");
            return;
        }

        int indiceAleatorio = Random.Range(0, listaDeReels.Length);
        uiImageComponent.sprite = listaDeReels[indiceAleatorio];
    }

    private void AdicionarTempo(int minutosPassados)
    {
        // Mostra no console para testar se o arrasto funcionou
        Debug.Log($"[DoomScrolling] Passou de Reel! +{minutosPassados} minutos no jogo.");

        // script de tempo chama ele aqui.
        // Exemplo: GameManager.Instance.PassarTempo(minutosPassados);
    }
}