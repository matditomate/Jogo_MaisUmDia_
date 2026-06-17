using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueMetro : MonoBehaviour
{
    [Header("Cenas")]
    [SerializeField] private string cenaVagao;
    [SerializeField] private string cenaSalaDeAula = "SaladeAula";

    [Header("Diálogos Ink")]
    [SerializeField] private string dialogoAntesDoVagao = "estacao_metro";
    [SerializeField] private string dialogoDepoisDoMinigame = "depois_minigame_ansiedade";

    private static bool voltouDoVagao = false;

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
        else
        {
            cenaDestino = cenaVagao;

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
}