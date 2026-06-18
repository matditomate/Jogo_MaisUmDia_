using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueMetro : MonoBehaviour
{
    [Header("Cenas")]
    [SerializeField] private string cenaVagao;
    [SerializeField] private string cenaSalaDeAula = "SaladeAula";
    [SerializeField] private string cenaSaladoRobin = "SalaRobinTeste";

    [Header("Diálogos Ink")]
    [SerializeField] private string dialogoAntesDoVagao = "estacao_metro";
    [SerializeField] private string dialogoDepoisDoMinigame = "depois_minigame_ansiedade";
    [SerializeField] private string dialogoDepoisDaAula = "depois_aula";

    private static bool voltouDoVagao = false;

    private static bool voltouDaAula = false;

    private bool jaTrocouCena = false;
    private string cenaDestino;

    private void Start()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager não encontrado na cena.");
            return;
        }

        if (voltouDoVagao)
        {
            voltouDoVagao = false;

            cenaDestino = cenaSalaDeAula;

            DialogueManager.Instance.StartDialogue(dialogoDepoisDoMinigame, AoTerminarDialogo);
        }
        else if (voltouDaAula)
        {
            voltouDaAula = false;

            cenaDestino = cenaSaladoRobin;

            DialogoChegouCasa.MarcarVoltaCasa();

            DialogueManager.Instance.StartDialogue(dialogoDepoisDaAula, AoTerminarDialogo);
        }
        else
        {
            cenaDestino = cenaVagao;

            GameManager.SetHorario(8.5f);

            DialogueManager.Instance.StartDialogue(dialogoAntesDoVagao, AoTerminarDialogo);
        }
    }

    private void AoTerminarDialogo(string resultado)
    {
        if (jaTrocouCena)
            return;

        jaTrocouCena = true;

        if (string.IsNullOrEmpty(cenaDestino))
        {
            Debug.LogError("Cena de destino não foi configurada no Inspector.");
            return;
        }

        SceneManager.LoadScene(cenaDestino);
    }

    public static void MarcarVoltaDoVagao()
    {
        voltouDoVagao = true;
    }

    public static void MarcarVoltaDaAula()
    {
        voltouDaAula = true;
    }
}