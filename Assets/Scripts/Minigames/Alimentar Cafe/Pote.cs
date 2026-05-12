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

    public AudioSource audioSource;
    public AudioClip somAcerto;

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

        // TOCAR SOUND EFFECT poteRacao
        if(audioSource != null && somAcerto != null)
        {
            audioSource.PlayOneShot(somAcerto);
        }

        pontos++;
        Debug.Log("Acertou: " + pontos);

        float porcentagem = (float)pontos / pontosMax * 10;

        Debug.Log(porcentagem);

        if(porcentagem <= 3.4)
        {
            poteUI.sprite = estadosPote[1];
        } else if (porcentagem <= 6.7)
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
        if (finalizado) return;
        finalizado = true;

        Debug.Log("WIN!");

        // 1. PARA LÓGICA DO JOGO
        enabled = false;
        RacaoDrop.jogoAtivo = false;

        // 2. PARA SCOOP
        if (scoop != null)
        {
            scoop.minigameAtivo = false;
            scoop.enabled = false;
        }

        // 3. BLOQUEIA O GATO (Usando a lógica estática que criamos)
        // Chamamos direto pela Classe Cafe para garantir a persistência entre cenas
        Cafe.AlterarFome(-4);
        Cafe.AlterarAtencao(2);
        Cafe.SetLocked(true);

        // 4. DESATIVA O MINIGAME
        // Aqui ainda usamos a referência 'cafe' para chegar no painel que está na cena
        if (cafe != null && cafe.minigamePanel != null)
        {
            cafe.minigamePanel.FecharPanel();
        }
        else
        {
            // Caso a referência 'cafe' falhe, você pode tentar fechar pelo PanelCafeOpener direto se tiver a referência
            Debug.LogWarning("Minigame fechado, mas a referência do script Cafe no PoteController sumiu!");
        }
    }
}