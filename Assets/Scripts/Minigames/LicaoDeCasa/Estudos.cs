using UnityEngine;
using UnityEngine.UI;

public class Estudos : MonoBehaviour
{
    [Header("Sprites de Evolução")]
    [SerializeField] private Sprite sprite33Porcento;
    [SerializeField] private Sprite sprite66Porcento;
    [SerializeField] private Sprite sprite99Porcento;

    [Header("Configurações do Clicker")]
    [SerializeField] private float reducaoPorClique = 0.25f; 
    [SerializeField] private float taxaCrescimento = 0.3f;   
    [SerializeField] private float escalaMaxima = 3.0f;      

    private int cliquesTotais = 0;
    private float tempoSemClique = 0f;

    private Image imagemComponente;
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        imagemComponente = GetComponent<Image>();

        if (imagemComponente != null)
        {
            imagemComponente.sprite = sprite33Porcento;
            imagemComponente.SetNativeSize();
            rect.sizeDelta *= 3f; 
        }
        
        rect.localScale = Vector3.one; 
    }

    void Update()
    {
        tempoSemClique += Time.deltaTime;

        // Se passar 3 segundos sem clique, começa a inchar
        if (tempoSemClique >= 3f)
        {
            if (rect.localScale.x < escalaMaxima)
            {
                Vector3 crescimento = new Vector3(taxaCrescimento, taxaCrescimento, 0f) * Time.deltaTime;
                rect.localScale += crescimento;
            }
        }
    }

    public void ClicouNaBola()
    {
        tempoSemClique = 0f;
        cliquesTotais++;
        
        // Impede que a escala inverta e fique negativa
        if (rect.localScale.x > 0.2f) 
        {
            rect.localScale -= new Vector3(reducaoPorClique, reducaoPorClique, 0f);
        }

        AtualizarFase();
    }

    void AtualizarFase()
    {
        if (cliquesTotais == 4)
        {
            AplicarSprite(sprite66Porcento);
        }
        else if (cliquesTotais == 7)
        {
            AplicarSprite(sprite99Porcento);
        }
        else if (cliquesTotais >= 10)
        {
            Destroy(gameObject); // Some da tela
        }
    }

    void AplicarSprite(Sprite novoSprite)
    {
        if (imagemComponente == null || novoSprite == null) return;

        imagemComponente.sprite = novoSprite;
        imagemComponente.SetNativeSize();
        rect.sizeDelta *= 3f; 
        rect.localScale = Vector3.one; // Reseta o tamanho pra próxima fase
    }
}