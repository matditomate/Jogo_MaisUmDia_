using UnityEngine;

public class Atalhos : MonoBehaviour
{

    [SerializeField] private RectTransform rt;
    private Celular scriptCel;
    private HoverCelular scriptHoverCel;
    [SerializeField] private GameObject background;

    void Start()
    {
        scriptCel = Object.FindAnyObjectByType<Celular>();
        scriptHoverCel = Object.FindAnyObjectByType<HoverCelular>();
    }

    public void OnPointerClickEffect()
    {
        scriptHoverCel.celUsando = false;

        rt.anchoredPosition = new Vector3(308.2f, -391f, 0f);
        Debug.Log(scriptCel);
        scriptCel.DesligarCelular();
        background.SetActive(false);

        CameraPanLateral.minigameAtivo = false;
    }
}
