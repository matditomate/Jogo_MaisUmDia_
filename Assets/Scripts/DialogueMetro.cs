using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueMetro : MonoBehaviour
{
    [Header("Cenas")]
    [SerializeField] private string cenaVagao;
    [SerializeField] private string cenaSalaDeAula = "SaladeAula";
    [SerializeField] private string cenaSaladoRobin = "SalaRobinTeste";

    [Header("Diálogos Ink")]
    [SerializeField] private string dialogoAntesDoVagao = "1_segunda_metro";
    [SerializeField] private string dialogoDepoisDoMinigame = "1_segunda_metro2";
    [SerializeField] private string dialogoMetroVolta = "1_segunda_metro_volta";

    private static bool voltouDoVagao = false;

    private static bool voltouDaAula = false;
    private static bool chegouAtrasado;

    private bool jaTrocouCena = false;
    private string cenaDestino;

    private void Start()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager não encontrado na cena.");
            return;
        }

        string dialogo;
        if (voltouDoVagao)
        {
            voltouDoVagao = false;

            cenaDestino = cenaSalaDeAula;

            dialogo = dialogoDepoisDoMinigame;
        }
        else if (voltouDaAula)
        {
            voltouDaAula = false;

            cenaDestino = cenaSaladoRobin;

            DialogoChegouCasa.MarcarVoltaCasa();

            dialogo = dialogoMetroVolta;
        }
        else
        {
            cenaDestino = cenaVagao;

            if (!chegouAtrasado)
                GameManager.instance.tempo.DefinirHorario(8.5f);

            chegouAtrasado = false;

            dialogo = dialogoAntesDoVagao;
        }

        int dia = GameManager.instance.tempo.Dia;
        if (dia == 1)
        {
            DialogueManager.Instance.StartDialogue(dialogo, AoTerminarDialogo);
        }
        else
        {
            AoTerminarDialogo(null);
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

    public static void IrAoMetroAtrasado(string cenaEstacao)
    {
        chegouAtrasado = true;
        SceneManager.LoadScene(cenaEstacao);
    }

    public static void VoltarDaAula(string cenaEstacao)
    {
        if (string.IsNullOrEmpty(cenaEstacao))
        {
            Debug.LogError("Cena da estação não foi configurada no Inspector.");
            return;
        }

        voltouDaAula = true;
        SceneManager.LoadScene(cenaEstacao);
    }
}
