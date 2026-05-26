using UnityEngine;
using UnityEngine.UI; 

public class RegadorController : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image imagemRegador; // Guarda a referência do componente de imagem

    [SerializeField] private GameObject particulaAgua; // O seu DropsObject
    
    [Header("Sprites do Regador")]
    [SerializeField] private Sprite spriteNormal;   // PNG dele em pé/parado
    [SerializeField] private Sprite spriteClicado;  // PNG dele inclinado/regando

    public bool minigameAtivo = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        imagemRegador = GetComponent<Image>();

        // Começa com o sprite normal garantido
        if (spriteNormal != null) imagemRegador.sprite = spriteNormal;
    }

    void Update()
    {
        if (!minigameAtivo) return;

        // Faz o regador seguir o mouse
        Vector2 posicaoMouse;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform, 
            Input.mousePosition, 
            null, 
            out posicaoMouse
        );
        rectTransform.anchoredPosition = posicaoMouse;

        // Detecta o clique para trocar o visual
        if (Input.GetMouseButton(0))
        {
            // Ativa o DropsObject (suas gotas)
            if (particulaAgua != null) particulaAgua.SetActive(true);

            // Troca o desenho do regador para o inclinado
            if (spriteClicado != null) imagemRegador.sprite = spriteClicado;
        }
        else
        {
            // Esconde o DropsObject
            if (particulaAgua != null) particulaAgua.SetActive(false);

            // Volta o regador para o sprite normal em pé
            if (spriteNormal != null) imagemRegador.sprite = spriteNormal;
        }
    }
}