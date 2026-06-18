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

    [Header("Referência do Jogo")]
    public Cafe cafe;

    public void AbrirPanel()
    {
        Debug.Log("[PANEL] Abrindo minigame");
        CameraPanLateral.minigameAtivo = true; // Congela camera e portas

        if (CursorMao != null)
        {
            CursorMao.DesativarCursor();
        }

        // Libera o cursor do sistema operacional
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;

        minigamePanel.SetActive(true);

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        if (scoop != null)
        {
            scoop.transform.SetParent(minigamePanel.transform, false);
            scoop.transform.SetAsLastSibling();
            scoop.gameObject.SetActive(true);
            scoop.minigameAtivo = true;
            scoop.cheio = false;
            scoop.enabled = true; 
        }
    }

    public void FecharPanel()
    {
        Debug.Log("[PANEL] Fechando minigame");
        CameraPanLateral.minigameAtivo = false; // Descongela camera e portas

        if (CursorMao != null)
        {
            CursorMao.AtivarCursor();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1f;

        // Centraliza a desativação do Scoop aqui
        if (scoop != null)
        {
            scoop.minigameAtivo = false;
            scoop.cheio = false;
            scoop.enabled = false;
            scoop.gameObject.SetActive(false); // Garante que o objeto do scoop suma da tela
        }

        minigamePanel.SetActive(false);

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}