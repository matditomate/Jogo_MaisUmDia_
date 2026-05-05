using UnityEngine;

public class WinPopupController : MonoBehaviour
{
    public GameObject popup;
    public PanelCafeOpener panelOpener;

    void Awake()
    {
        if (popup != null)
        {
            popup.SetActive(false);
        }
    }

    public void Show()
    {
        Debug.Log("[WIN] Mostrando Popup de Vitória");

        popup.SetActive(true);

        // Tenta pegar o CanvasGroup para garantir que esteja visível
        CanvasGroup cg = popup.GetComponent<CanvasGroup>();
        if (cg == null) cg = popup.GetComponentInChildren<CanvasGroup>();

        if (cg != null)
        {
            cg.alpha = 1f;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        // Centraliza o popup e coloca na frente de outros elementos do painel
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

        // Quando fechar a vitória, chama o PanelOpener para fechar o minijogo todo
        if (panelOpener != null)
        {
            panelOpener.FecharPanel();
        }
    }
}