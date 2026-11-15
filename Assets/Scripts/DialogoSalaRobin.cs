using UnityEngine;

[RequireComponent(typeof(Porta))]
public class DialogoSalaRobin : MonoBehaviour
{
    [SerializeField] private string dialogoSaida = "preparar_para_faculdade";

    public void AoClicarPortaMetro()
    {
        if (CameraPanLateral.minigameAtivo)
            return;

        var dialogos = DialogueManager.Instance;
        if (dialogos != null && dialogos.IsDialogueActive())
            return;

        var porta = GetComponent<Porta>();
        int dia = GameManager.instance.tempo.Dia;
        if (dia == 1)
        {
            if (dialogos == null)
            {
                Debug.LogError("DialogueManager não encontrado na sala da Robin.", this);
                return;
            }

            dialogos.StartDialogue(dialogoSaida, _ => porta.IrProximaFase());
        }
        else
        {
            porta.IrProximaFase();
        }
    }
}
