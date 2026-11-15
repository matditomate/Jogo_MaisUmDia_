using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogoVagao : MonoBehaviour
{
    [Header("Cena para voltar")]
    [SerializeField] private string cenaEstacao = "Estacao";

    [Header("Diálogo Ink")]
    [SerializeField] private string dialogoInk = "1_segunda_vagao";

    [SerializeField] private HudManager hudGlobalPrefab;

    private bool jaTrocouCena = false;
    private bool jaTeveMinigame = false;

    private void Start()
    {
        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager não encontrado na cena.");
            return;
        }

        int dia = GameManager.instance.tempo.Dia;
        if (dia == 1)
        {
            DialogueManager.Instance.StartDialogue(dialogoInk, AoTerminarDialogo);
        }
        else
        {
            AoTerminarDialogo(null);
        }
    }
    
    private void Update()
    {
        if (jaTeveMinigame && DataManager.instance.ansiedadeRobin < 10)
            PosMinigame();
    }

    private void AoTerminarDialogo(string resultado)
    {
        if (jaTeveMinigame)
            return;

        if (HudManager.instance == null || !HudManager.instance.ProntoParaAnsiedade)
        {
            if (hudGlobalPrefab == null || !hudGlobalPrefab.ProntoParaAnsiedade)
            {
                Debug.LogError("O HUD do vagão está sem o painel do minigame de ansiedade.", this);
                return;
            }

            Instantiate(hudGlobalPrefab);
        }

        jaTeveMinigame = true;
        GameManager.instance.statusRobin.AlterarAnsiedade(10);
    }

    private void PosMinigame()
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
