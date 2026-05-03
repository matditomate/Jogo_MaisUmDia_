using UnityEngine;

public class WinPopupController : MonoBehaviour
{
    public GameObject popup;
    public PanelCafeOpener panelOpener;

    private CanvasGroup cg;

    void Awake()
    {
        if (popup != null)
        {
            cg = popup.GetComponent<CanvasGroup>();

            if (cg == null)
                cg = popup.GetComponentInChildren<CanvasGroup>();

            popup.SetActive(false);
        }
    }

    public void Show()
    {
        Debug.Log("SHOW CHAMADO");

        popup.SetActive(true);

        CanvasGroup cg = popup.GetComponent<CanvasGroup>();

        if (cg == null)
            cg = popup.GetComponentInChildren<CanvasGroup>();

        if (cg != null)
        {
            cg.alpha = 1f;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
        else
        {
            Debug.LogWarning("Sem CanvasGroup!");
        }

        RectTransform rt = popup.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.localScale = Vector3.one;
            rt.anchoredPosition = Vector2.zero;
        }

        popup.transform.SetAsLastSibling();
    }
    public void Close()
    {
        popup.SetActive(false);

        if (panelOpener != null)
            panelOpener.FecharPanel();
    }
}