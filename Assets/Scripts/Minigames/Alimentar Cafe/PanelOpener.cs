using UnityEngine;

public class PanelCafeOpener : MonoBehaviour
{
    public GameObject worldRoot;
    public GameObject minigamePanel;

    public CanvasGroup canvasGroup;
    public ScoopController scoop;

    public Cafe cafe;

    public void AbrirPanel()
    {
        Debug.Log("[PANEL] Abrindo minigame");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        worldRoot.SetActive(false);
        minigamePanel.SetActive(true);

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        scoop.transform.SetParent(minigamePanel.transform, false);
        scoop.transform.SetAsLastSibling();

        scoop.gameObject.SetActive(true);

        scoop.minigameAtivo = true;
        scoop.cheio = false;
    }

    public void FecharPanel()
    {
        Debug.Log("[PANEL] Fechando minigame");

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        worldRoot.SetActive(true);

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