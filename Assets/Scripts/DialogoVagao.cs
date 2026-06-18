using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueVagao : MonoBehaviour
{
    [Header("Cena para voltar")]
    [SerializeField] private string cenaEstacao = "Estacao";

    [Header("Diálogo Ink")]
    [SerializeField] private string dialogoInk = "estacao_vagao";

    private bool jaTrocouCena = false;

    private void Start()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager não encontrado na cena.");
            return;
        }

        DialogueManager.Instance.StartDialogue(dialogoInk, AoTerminarDialogo);
    }

    private void AoTerminarDialogo(string resultado)
    {
        if (jaTrocouCena)
            return;

        jaTrocouCena = true;

        DialogueMetro.MarcarVoltaDoVagao();

        if (string.IsNullOrEmpty(cenaEstacao))
        {
            Debug.LogError("Nome da cena da estação não foi colocado no Inspector.");
            return;
        }

        SceneManager.LoadScene(cenaEstacao);
    }
}