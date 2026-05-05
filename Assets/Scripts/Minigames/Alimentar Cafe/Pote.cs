using UnityEngine;
using UnityEngine.UI;

public class PoteController : MonoBehaviour
{
    [SerializeField] private Image poteUI;
    [SerializeField] private Sprite[] estadosPote;
    public RectTransform rect;
    public float speed = 200f;

    public float limiteX = 300f;
    private int direcao = 1;

    public int pontos = 0;
    public int pontosMax = 3;

    public WinPopupController winPopup;
    public ScoopController scoop;
    public Cafe cafe;

    private bool finalizado = false;

    void Start()
    {
        if (rect == null)
            rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (finalizado) return;

        rect.anchoredPosition += new Vector2(direcao * speed * Time.deltaTime, 0);

        if (rect.anchoredPosition.x > limiteX)
            direcao = -1;
        else if (rect.anchoredPosition.x < -limiteX)
            direcao = 1;
    }

    public void Acertou()
    {
        if (finalizado) return;

        pontos++;
        Debug.Log("Acertou: " + pontos);

        int porcentagem = pontos / pontosMax * 10;

        if(porcentagem <= 3)
        {
            poteUI.sprite = estadosPote[1];
        } else if (porcentagem <= pontosMax)
        {
            poteUI.sprite = estadosPote[2];
        }

        if (pontos >= pontosMax)
        {    
            Vencer();
        }
    }

    void Vencer()
    {
        Debug.Log("WIN!");

        if (finalizado) return;
        finalizado = true;

        // 1. PARA LÓGICA DO JOGO
        enabled = false;
        RacaoDrop.jogoAtivo = false;

        // 2. PARA SCOOP
        if (scoop != null)
        {
            scoop.minigameAtivo = false;
            scoop.enabled = false;
        }

        // 3. BLOQUEIA O GATO 
        if (cafe != null)
        {
            cafe.SetFull();
            cafe.LockCat();
        }

        // 4. DESATIVA O MINIGAME
        if (cafe != null && cafe.minigamePanel != null)
            cafe.minigamePanel.FecharPanel();
    }
}