using UnityEngine;
using UnityEngine.UI; 

public class RegadorController : MonoBehaviour
{
    private Animator anim;
    private RectTransform rectTransform;
    private Image imagemRegador; 

    [SerializeField] private GameObject particulaAgua; 
    
    [Header("Sprites do Regador")]
    [SerializeField] private Sprite spriteNormal;   
    [SerializeField] private Sprite spriteClicado;  

    public bool minigameAtivo = true;

    private void OnEnable()
    {
        if (imagemRegador == null) imagemRegador = GetComponent<Image>();

        // Força o regador a começar resetado e correto
        if (spriteNormal != null) imagemRegador.sprite = spriteNormal;
        if (particulaAgua != null) particulaAgua.SetActive(false);
        minigameAtivo = true;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!minigameAtivo) return;

        // Faz o regador seguir o mouse perfeitamente no Canvas
        Vector2 posicaoMouse;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform, 
            Input.mousePosition, 
            null, 
            out posicaoMouse
        );
        rectTransform.anchoredPosition = posicaoMouse;

        // Troca o visual e as partículas baseado no clique
        if (Input.GetMouseButton(0))
        {
            // if (particulaAgua != null) particulaAgua.SetActive(true);
            // if (spriteClicado != null) imagemRegador.sprite = spriteClicado;
            anim.SetBool("regando", true);
        }
        else
        {
            // if (particulaAgua != null) particulaAgua.SetActive(false);
            // if (spriteNormal != null) imagemRegador.sprite = spriteNormal;
            anim.SetBool("regando", false);
        }
    }
}