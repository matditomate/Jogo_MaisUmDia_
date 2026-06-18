using UnityEngine;
using UnityEngine.UI;

public class Celular : MonoBehaviour
{

    [SerializeField] private GameObject inicio;
    [SerializeField] private Image fundoUI;
    [SerializeField] private Sprite[] fundos;
    [SerializeField] private GameObject telaDesbloq;
    [SerializeField] private Desbloquear scriptDesbloq;
    

    public void LigarTela()
    {
        inicio.SetActive(true);
        fundoUI.sprite = fundos[1];
    }

    public void DesligarTela()
    {
        inicio.SetActive(false);
        fundoUI.sprite = fundos[0];
    }

    public void TelaDesbloqueada()
    {
        inicio.SetActive(false);
        telaDesbloq.SetActive(true);
    }

    public void DesligarCelular()
    {
        telaDesbloq.SetActive(false);
        scriptDesbloq.tAtualDesbloq = 0f;
        scriptDesbloq.desbloqueando = false;
        fundoUI.sprite = fundos[0];
    }
}
