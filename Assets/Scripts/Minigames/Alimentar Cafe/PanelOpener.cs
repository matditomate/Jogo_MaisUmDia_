using UnityEngine;

public class PanelCafeOpener : MonoBehaviour
{
    [Header("Configurações de Objetos")]
    public GameObject worldRoot;
    public GameObject minigamePanel;
    public CursorCustom CursorMao;

    [Header("Componentes de Interface")]
    public CanvasGroup canvasGroup;
    public ScoopController scoop;

    [Header("Tutorial (opcional)")]
    public TutorialPanel tutorial;   // deixe o "Ao Finalizar" dele VAZIO

    [Header("Referência do Jogo")]
    public Cafe cafe;

    private void Start()
    {
        if (tutorial != null)
            tutorial.aoFinalizar.AddListener(IniciarMinigame);
    }

    public void AbrirPanel()
    {
        // Gato travado: não abre minigame nem tutorial
        if (GameManager.instance.statusCafe.Locked)
        {
            Debug.Log("[PANEL] Café está travado, minigame não abre.");
            return;
        }

        Debug.Log("[PANEL] Abrindo minigame");
        CameraPanLateral.minigameAtivo = true;

        if (CursorMao != null)
            CursorMao.DesativarCursor();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        minigamePanel.SetActive(true);

        // O PoteController liga a ração no OnEnable; seguramos até o tutorial fechar
        RacaoDrop.jogoAtivo = false;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        if (scoop != null)
        {
            scoop.transform.SetParent(minigamePanel.transform, false);
            scoop.transform.SetAsLastSibling();
            scoop.gameObject.SetActive(true);
            scoop.minigameAtivo = false;
            scoop.cheio = false;
            scoop.enabled = false;
        }

        if (tutorial != null)
            tutorial.Mostrar();      // se já foi visto, chama IniciarMinigame() na hora
        else
            IniciarMinigame();
    }

    public void IniciarMinigame()
    {
        RacaoDrop.jogoAtivo = true;

        if (scoop != null)
        {
            scoop.minigameAtivo = true;
            scoop.enabled = true;
        }
    }

    public void FecharPanel()
    {
        Debug.Log("[PANEL] Fechando minigame");
        CameraPanLateral.minigameAtivo = false; // Descongela camera e portas

        if (CursorMao != null)
            CursorMao.AtivarCursor();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1f;

        if (scoop != null)
        {
            scoop.minigameAtivo = false;
            scoop.cheio = false;
            scoop.enabled = false;
            scoop.gameObject.SetActive(false);
        }

        minigamePanel.SetActive(false);

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}