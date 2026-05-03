using UnityEngine;

public class RacaoDrop : MonoBehaviour
{
    public PoteController pote;
    public RectTransform poteArea;
    public RectTransform rect;

    public float speed = 300f;
    public static bool jogoAtivo = true;
    void Update()
    {
        if (!jogoAtivo) return;
        
        // cai no eixo Y (UI SPACE)
        rect.anchoredPosition += Vector2.down * speed * Time.deltaTime;

        // checagem correta também em UI space
        if (RectTransformUtility.RectangleContainsScreenPoint(
            poteArea,
            RectTransformUtility.WorldToScreenPoint(null, rect.position),
            null))
        {
            pote.Acertou();
            Destroy(gameObject);
        }
    }
}