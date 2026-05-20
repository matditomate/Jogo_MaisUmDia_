using UnityEngine;

public class BalaoSpawner : MonoBehaviour
{
    public GameObject prefabBalao;

    public RectTransform canvas;

    public float intervalo = 0.5f;

    public float minY = 350f;
    public float maxY = 360f;

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
            347f,
            Random.Range(minY, maxY)
        );

        Baloes script =
            balao.GetComponent<Baloes>();

        float valor = Random.Range(-1f, 1f);

        float resultado1 =
            Mathf.Sign(valor) *
            Mathf.Pow(Mathf.Abs(valor), 2f) *
            2f; 

        float resultado2 =
            Mathf.Sign(valor) *
            Mathf.Pow(Mathf.Abs(valor), 0.5f) *
            2f;

        float resultadoF = (resultado1 + resultado2) / 2f;

        script.modificador = resultadoF;
    }
}