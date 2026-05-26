using UnityEngine;

public class PanelCafeOpener : MonoBehaviour
{
    [Header("Configurações de Objetos")]
    // Removi a desativação do worldRoot para que ele continue visível ao fundo
    public GameObject worldRoot; 
    public GameObject minigamePanel;

    public CursorCustom CursorMao;

    [Header("Componentes de Interface")]
    public CanvasGroup canvasGroup;
    public ScoopController scoop;

    [Header("Referência do Jogo")]
    public Cafe cafe;

    public void AbrirPanel()
    {
        Debug.Log("[PANEL] Abrindo minigame");
        CameraPanLateral.minigameAtivo = true; // CONGELA A CÂMERA E AS PORTAS!

        if (CursorMao != null)
        {
            CursorMao.DesativarCursor();
        }

        // Liberar o cursor para interagir com o Canvas
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        // Opcional: Pausa o tempo do jogo (física, timers, etc)
        // Time.timeScale = 0f; 

        // Ativamos o painel do minigame
        minigamePanel.SetActive(true);

        // Configuramos o CanvasGroup para ser visível e interativo
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        // Configuração do objeto "Scoop" (concha/colher)
        if (scoop != null)
        {
            scoop.transform.SetParent(minigamePanel.transform, false);
            scoop.transform.SetAsLastSibling();
            scoop.gameObject.SetActive(true);
            scoop.minigameAtivo = true;
            scoop.cheio = false;
            scoop.enabled = true; // Garante que o script do scoop funcione
        }
    }

    public void FecharPanel()
    {
        Debug.Log("[PANEL] Fechando minigame");
        CameraPanLateral.minigameAtivo = false; // DESCONGELA A CÂMERA E AS PORTAS!

        // 2. REATIVA o cursor customizado (CursorMao) ao sair do minigame
        if (CursorMao != null)
        {
            CursorMao.AtivarCursor();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1f;

        if (scoop != null)
        {
            scoop.minigameAtivo = false;
            scoop.cheio = false;
            scoop.enabled = false;
        }

        minigamePanel.SetActive(false);

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}