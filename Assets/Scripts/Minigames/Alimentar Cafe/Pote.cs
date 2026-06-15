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
    private Casa scriptCasa;

    // O OnEnable roda toda vez que o minigame abre na tela
    private void OnEnable()
    {
        pontos = 0;
        finalizado = false;
        RacaoDrop.jogoAtivo = true; // Ativa a queda dos prefabs novamente

        // Reseta o sprite do pote para o primeiro estado (vazio)
        if (poteUI != null && estadosPote != null && estadosPote.Length > 0)
        {
            poteUI.sprite = estadosPote[0];
        }
    }

    void Start()
    {
        scriptCasa = Object.FindAnyObjectByType<Casa>();
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

        if (audioSource != null && somAcerto != null)
        {
            AudioManager.instance.TocarSFX(somAcerto);
        }

        pontos++;
        Debug.Log("Acertou: " + pontos);

        float porcentagem = (float)pontos / pontosMax * 10;

        if (porcentagem <= 3.4)
        {
            poteUI.sprite = estadosPote[1];
        }
        else if (porcentagem <= 6.7)
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

        // Paramos o jogo (Não precisamos de enabled = false, o FecharPanel já vai congelar tudo)
        RacaoDrop.jogoAtivo = false;

        if (scoop != null)
        {
            scoop.minigameAtivo = false;
            scoop.enabled = false;
        }

        // Status do gato e horário
        Cafe.AlterarFome(4);
        Cafe.AlterarAtencao(2);
        Cafe.SetLocked(true);
        scriptCasa.AlterarHoraio(0.25f);

        if (cafe != null && cafe.minigamePanel != null)
        {
            cafe.minigamePanel.FecharPanel();
        }
        else
        {
            Debug.LogWarning("Minigame fechado, mas a referência do script Cafe no PoteController sumiu!");
        }
    }
}