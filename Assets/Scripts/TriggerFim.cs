using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFim : MonoBehaviour
{
    [SerializeField] private string nomeProximaFase;
    public AudioClip somFim;
    public void IrParaFim()
    {
        GameManager.instance.som.TocarSFX(somFim);
        SceneManager.LoadScene(this.nomeProximaFase);
    }
}