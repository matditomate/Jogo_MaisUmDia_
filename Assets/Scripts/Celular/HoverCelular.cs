using UnityEngine;
using UnityEngine.UI;

public class HoverCelular : MonoBehaviour
{
    private RectTransform rt;
    public Celular scriptCel;
    public bool celUsando = false;
    [SerializeField] private GameObject background;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(308.2f, -391f, 0f);
        scriptCel.DesligarTela();
    }

    public void OnHoverEnterEffect()
    {
        if(celUsando) return;
        rt.anchoredPosition = new Vector2(308.2f, -343.6f);
        scriptCel.LigarTela();
    }

    public void OnHoverExitEffect()
    {
        if(celUsando) return;
        rt.anchoredPosition = new Vector3(308.2f, -391f, 0f);
        scriptCel.DesligarTela();
    }

    public void OnPointerClickEffect()
    {
        if(celUsando) return;
        celUsando = true;
        rt.anchoredPosition = new Vector3(0f, -35f, 0f);
        scriptCel.LigarTela();
        background.SetActive(true);

        CameraPanLateral.minigameAtivo = true;
    }

}
