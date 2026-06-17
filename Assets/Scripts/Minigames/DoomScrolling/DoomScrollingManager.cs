using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class DadosPost
{
    public string nomeDaImagem;
    public string usuario;
    public string legenda;
}

[System.Serializable]
public class PostBaseDados
{
    public DadosPost[] posts;
}

public class DoomScrollingManager : MonoBehaviour, IDragHandler
{
    [Header("Configurações do Feed")]
    [Tooltip("Arraste os 3 slots que ficam rodando na UI")]
    public RectTransform[] scrollElements; 

    private DadosPost[] meusPosts; 
    private float elementHeight;
    private float boundaryY;

    void Start()
    {
        CarregarBancoDeDados();

        if (scrollElements.Length > 0 && meusPosts != null)
        {
            elementHeight = scrollElements[0].rect.height;
            boundaryY = (scrollElements.Length / 2f) * elementHeight;

            foreach (var element in scrollElements)
            {
                MostrarReelAleatorio(element);
            }
        }
    }

    private void CarregarBancoDeDados()
    {
        TextAsset arquivoJson = Resources.Load<TextAsset>("dados_posts");

        if(arquivoJson != null)
        {
            PostBaseDados db = JsonUtility.FromJson<PostBaseDados>(arquivoJson.text);
            meusPosts = db.posts;
        }
        else
        {
            Debug.LogError("Arquivo JSON 'dados_posts' não encontrado na pasta Resources!");
        }
    }

    // Detecta o momento exato do clique
    public void OnDrag(PointerEventData eventData)
    {
        float dragAmount = eventData.delta.y;

        // 1. Move todas as peças da esteira
        for (int i = 0; i < scrollElements.Length; i++)
        {
            scrollElements[i].anchoredPosition += new Vector2(0, dragAmount);
        }

        // 2. Checa se alguma peça saiu da tela para reciclar
        for (int i = 0; i < scrollElements.Length; i++)
        {
            if (scrollElements[i].anchoredPosition.y > boundaryY)
            {
                MoveToBottom(scrollElements[i]);
                MostrarReelAleatorio(scrollElements[i]);
                
                // O jogador passou de post (arrastou para cima)! Consome tempo.
                AdicionarTempo(5); 
            }
            else if (scrollElements[i].anchoredPosition.y < -boundaryY)
            {
                MoveToTop(scrollElements[i]);
                MostrarReelAleatorio(scrollElements[i]);
                
                // (Opcional) Pode adicionar tempo aqui também caso ele volte posts
            }
        }
    }

    private void MoveToBottom(RectTransform element)
    {
        float lowestY = element.anchoredPosition.y;
        for (int i = 0; i < scrollElements.Length; i++)
        {
            if (scrollElements[i].anchoredPosition.y < lowestY)
                lowestY = scrollElements[i].anchoredPosition.y;
        }
        element.anchoredPosition = new Vector2(element.anchoredPosition.x, lowestY - elementHeight);
    }

    private void MoveToTop(RectTransform element)
    {
        float highestY = element.anchoredPosition.y;
        for (int i = 0; i < scrollElements.Length; i++)
        {
            if (scrollElements[i].anchoredPosition.y > highestY)
                highestY = scrollElements[i].anchoredPosition.y;
        }
        element.anchoredPosition = new Vector2(element.anchoredPosition.x, highestY + elementHeight);
    }

    // private void AvançarFeed()
    // {
    //     MostrarReelAleatorio();
    //     AdicionarTempo(5); // Avisa ao jogo que se passaram 5 minutos
    // }

    private void MostrarReelAleatorio(RectTransform element)
    {
        if (meusPosts == null || meusPosts.Length == 0)
        {
            Debug.LogWarning("Nenhuma imagem foi colocada na lista de Reels do ImageReel!");
            return;
        }

        int indiceAleatorio = Random.Range(0, meusPosts.Length);
        DadosPost postEscolhido = meusPosts[indiceAleatorio];


        PostSlot slotConfig = element.GetComponent<PostSlot>();

        if(slotConfig != null)
        {
            slotConfig.PreencherDados(postEscolhido);  
        }
        else
        {
            Debug.LogError("Faltou anexar o script ReelSlot no elemento: " + element.name);
        }
    }

    private void AdicionarTempo(int minutosPassados)
    {
        // Mostra no console para testar se o arrasto funcionou
        Debug.Log($"[DoomScrolling] Passou de Reel! +{minutosPassados} minutos no jogo.");

        // script de tempo chama ele aqui.
        // Exemplo: GameManager.Instance.PassarTempo(minutosPassados);
    }
}