using UnityEngine;
using UnityEngine.UI;

public class Celular : MonoBehaviour
{

    [SerializeField] private GameObject inicio;
    [SerializeField] private Image fundoUI;
    [SerializeField] private Sprite[] fundos;
    [SerializeField] private GameObject telaDesbloq;


    public void ligarTela()
    {
        inicio.SetActive(true);
        fundoUI.sprite = fundos[1];
    }

    public void desligarTela()
    {
        inicio.SetActive(false);
        fundoUI.sprite = fundos[0];
    }

    public void telaDesbloqueada()
    {
        inicio.SetActive(false);
        telaDesbloq.SetActive(true);
    }
}
