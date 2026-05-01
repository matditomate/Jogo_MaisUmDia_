using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    [SerializeField] private string nomeProximaFase;
    [SerializeField] private string nomePortaDeDestino;

    public void IrProximaFase()
    {
        GameManager.instance.portaDeDestino = nomePortaDeDestino;
        SceneManager.LoadScene(this.nomeProximaFase);
    }
}
