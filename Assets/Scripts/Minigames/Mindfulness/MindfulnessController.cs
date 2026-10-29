using UnityEngine;

public class MindfulnessAppController : MonoBehaviour
{
    [SerializeField] private GameObject painelInterfaceMindfulness; // Arraste o painel principal do app aqui
    [SerializeField] private GameObject telaPretaMinigame;         // Arraste a tela preta aqui

    public void IniciarMinigame()
    {
        // Desativa a visibilidade da interface do app de mindfulness
        if (painelInterfaceMindfulness != null)
        {
            painelInterfaceMindfulness.SetActive(false);
        }

        // Ativa a visibilidade da tela preta / mini-game
        if (telaPretaMinigame != null)
        {
            telaPretaMinigame.SetActive(true);
        }
    }
}