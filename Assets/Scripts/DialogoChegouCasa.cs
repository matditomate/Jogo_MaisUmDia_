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

            int dia = GameManager.instance.tempo.Dia;
            if (dia == 1)
            {
                DialogueManager.Instance.StartDialogue("1_segunda_casa_tarde");
            }

            GameManager.instance.tempo.DefinirHorario(16.0f);
        }
    }

     public static void MarcarVoltaCasa()
    {
        voltouCasa = true;
    }
}
