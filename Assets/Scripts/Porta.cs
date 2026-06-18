using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    [SerializeField] private string nomeProximaFase;
    [SerializeField] private string nomePortaDeDestino;

    public AudioClip somAbrir;

    public void IrProximaFase()
    {
        // VERIFICAÇÃO: Se o minigame estiver ativo, impede o jogador de mudar de fase
        if (CameraPanLateral.minigameAtivo)
        {
            Debug.Log("Você não pode mudar de sala enquanto estiver no minigame!");
            return; // Corta a execução da função aqui e não muda de cena
        }
        
        //Som da porta ao mudar de cena
        AudioManager.instance.TocarSFX(somAbrir);
        AudioManager.instance.AgendarSomDeFechar();

        GameManager.instance.portaDeDestino = nomePortaDeDestino;
        SceneManager.LoadScene(this.nomeProximaFase);
    }
}
