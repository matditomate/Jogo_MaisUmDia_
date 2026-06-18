using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Baloes : MonoBehaviour
{
    public float velocidadeX = 400f;
    public float amplitude = 100f;
    public float frequencia = 2f;
    public float tempoDeVida = 3f;

    private RectTransform rect;
    private Image imagem;

    private float tempo;

    public float modificador;

    public bool tipoBalao;
    private Vector2 posicaoInicial;

    public TextMeshProUGUI Fala;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        imagem = GetComponent<Image>();

        posicaoInicial = rect.anchoredPosition;
    }

    void Update()
    {

        tempo += Time.deltaTime;
        
        if (tipoBalao)
        {
            // Anda continuamente
            rect.anchoredPosition += new Vector2(
                velocidadeX * Time.deltaTime * -1f,
                Mathf.Sin(tempo * frequencia) * amplitude * modificador * Time.deltaTime
            );
        }

        // Fade
        Color cor = imagem.color;

        cor.a = Mathf.Lerp(1f, 0f, tempo / tempoDeVida);

        imagem.color = cor;

        // Destrói
        if (cor.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Clicou()
    {
        BalaoManager.pontos++;
        if(Fala.color == Color.red)
        {
           BalaoManager.pontos--; 
        }
        else if(Fala.color == Color.green)
        {
            BalaoSpawner.instance.Pensamento = false;
            BalaoManager.pontos++;
        }
        else
        {
            BalaoManager.pontos++;
        }

        Destroy(gameObject);
    }
}