using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogoChegouCasa : MonoBehaviour
{
    private static bool voltouCasa = false;

    void Start()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager não encontrado na cena.");
            return;
        }

        if (voltouCasa)
        {
            voltouCasa = false;

            DialogueManager.Instance.StartDialogue("chegando_casa");
        }
    }

     public static void MarcarVoltaCasa()
    {
        voltouCasa = true;
    }
}