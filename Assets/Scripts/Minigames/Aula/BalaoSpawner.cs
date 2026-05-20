using UnityEngine;

public class BalaoSpawner : MonoBehaviour
{
    public GameObject prefabBalao;

    public RectTransform canvas;

    public float intervalo = 1f;

    public float minY = -300f;
    public float maxY = 300f;

    void Start()
    {
        InvokeRepeating(
            nameof(CriarBalao),
            1f,
            intervalo
        );
    }

    void CriarBalao()
    {
        GameObject balao = Instantiate(
            prefabBalao,
            canvas
        );

        RectTransform rect =
            balao.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(
            -900f,
            Random.Range(minY, maxY)
        );
    }
}