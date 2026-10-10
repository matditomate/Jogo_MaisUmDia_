using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueVagao : MonoBehaviour
{
    [Header("Cena para voltar")]
    [SerializeField] private string cenaEstacao = "Estacao";

    [Header("Diálogo Ink")]
    [SerializeField] private string dialogoInk = "estacao_vagao";

    private string result;

    private bool jaTrocouCena = false;
    private bool jaTeveMinigame = false;

    private void Start()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager não encontrado na cena.");
            return;
        }

        DialogueManager.Instance.StartDialogue(dialogoInk, AoTerminarDialogo);
    }
    
    private void Update()
    {
        AoTerminarDialogo(null);
    }

    private void AoTerminarDialogo(string resultado)
    {
        if(resultado != null) result = resultado;
        if(!jaTeveMinigame)
        {
            Robin.AlterarAnsiedade(10);
            
            jaTeveMinigame = true;
        }

        if(Robin.ansiedade != 10)
        {
            PosMinigame(resultado);
        }
    }

    private void PosMinigame(string resultado)
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