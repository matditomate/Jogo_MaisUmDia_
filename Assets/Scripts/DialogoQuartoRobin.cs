using UnityEngine;

public class DialogoQuartoRobin : MonoBehaviour
{
    private const string EscolhaAdiarDespertador = "despertador_snooze";

    [SerializeField] private string dialogoManha = "1_segunda_6_quarto";
    [SerializeField] private string cenaEstacao = "Estacao";
    [SerializeField] private AudioSource despertador;

    private void Start()
    {
        int dia = GameManager.instance.tempo.Dia;
        if (dia == 1 && !DataManager.instance.dialogoManhaQuartoConcluido)
        {
            var dialogos = DialogueManager.Instance;
            if (dialogos == null)
            {
                Debug.LogError("DialogueManager não encontrado no quarto da Robin.", this);
                return;
            }

            despertador.Play();
            dialogos.StartDialogue(dialogoManha, AoTerminarDialogo, AoEscolherDespertador);
        }
    }

    private void AoTerminarDialogo(string resultado)
    {
        PararDespertador();
        if (resultado == "despertador_levantar" || resultado == EscolhaAdiarDespertador)
            DataManager.instance.dialogoManhaQuartoConcluido = true;

        if (resultado == EscolhaAdiarDespertador)
            DialogueMetro.IrAoMetroAtrasado(cenaEstacao);
    }

    private void AoEscolherDespertador(string resultado)
    {
        PararDespertador();
        if (resultado == EscolhaAdiarDespertador)
            GameManager.instance.tempo.DefinirHorario(8f);
    }

    private void PararDespertador()
    {
        despertador.Stop();
    }
}
