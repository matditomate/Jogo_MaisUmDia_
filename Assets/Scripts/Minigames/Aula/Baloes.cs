using UnityEngine;
using UnityEngine.UI;

public class Baloes : MonoBehaviour
{
    public float velocidadeX = 200f;
    public float amplitude = 100f;
    public float frequencia = 2f;
    public float tempoDeVida = 1f;

    private RectTransform rect;
    private Image imagem;

    private float tempo;
    private Vector2 posicaoInicial;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        imagem = GetComponent<Image>();

        posicaoInicial = rect.anchoredPosition;
    }

    void Update()
    {
        tempo += Time.deltaTime;

        // Anda continuamente
        rect.anchoredPosition += new Vector2(
            velocidadeX * Time.deltaTime,
            Mathf.Sin(tempo * frequencia) * amplitude * Time.deltaTime
        );

        // Fade
        Color cor = imagem.color;

        cor.a = Mathf.Lerp(1f, 0f, tempo / tempoDeVida);

        imagem.color = cor;

        // Destrói
        if (tempo >= tempoDeVida)
        {
            Destroy(gameObject);
        }
    }

    public void Clicou()
    {
        BalaoManager.pontos++;

        Destroy(gameObject);
    }
}