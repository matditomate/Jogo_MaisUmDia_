using UnityEngine;

public class GerenciadorCenaFinal : MonoBehaviour
{
    void Start()
    {
        // Procura pelos objetos do Canvas do Bootstrap pelo nome na hierarquia
        GameObject hudNecessidades = GameObject.Find("HudNecessidades");
        GameObject hudCelular = GameObject.Find("HudCelular");

        // Se encontrar o HUD de necessidades, desativa
        if (hudNecessidades != null)
        {
            hudNecessidades.SetActive(false);
            Debug.Log("HUD de Necessidades escondido para a cena final.");
        }

        // Se encontrar o Celular, desativa
        if (hudCelular != null)
        {
            hudCelular.SetActive(false);
            Debug.Log("Hud do Celular escondido para a cena final.");
        }
    }
}