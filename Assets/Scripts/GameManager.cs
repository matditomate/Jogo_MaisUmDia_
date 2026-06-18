using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string portaDeDestino;

    public static int sujeira = 5; // 10 = muito suja

    public static int aguaPlanta = 6;

    public static float horario = 6.0f;

    public static int dia = 1;

    private Horario horarioScript;

    public static event Action OnHorarioMudou;

    public GameObject canvasAnsiedade;

    void Awake()
    {
        // Garante que só exista um GameManager no jogo
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        AlterarHorario(0);
        AtivarEfeitoAnsiedade();
    }

    public void AlterarSujeira(int valor)
    {
        sujeira = Mathf.Clamp(sujeira + valor, 0, 10);
        Debug.Log($"Sujeira da casa: {sujeira}");
    }

    public static void AlterarAguaPlanta(int valor)
    {
        aguaPlanta = Mathf.Clamp(aguaPlanta + valor, 0, 10);
        Debug.Log($"Água das plantas: {aguaPlanta}");
    }

    public static void SetHorario(float valor)
    {
        horario = valor;

        if (OnHorarioMudou != null)
        {
            OnHorarioMudou.Invoke();
        }
    }

    public void AlterarHorario(float valor)
    {
        horario += valor;
        if (horario > 23.98f)
        {
            horario -= 23.98f;
            dia += 1;
        }

        if (horario == 6.0f && dia == 1)
        {
            DialogueManager.Instance.StartDialogue("primeiro_dia", ReceberResultado);
        }

        if (OnHorarioMudou != null)
        {
            OnHorarioMudou.Invoke();
        }
    }

    private void ReceberResultado(string resultado)
    {
        Debug.Log("Resultado recebido do Ink: " + resultado);

        if (resultado == "despertador_levantar")
        {
            DialogueManager.Instance.StartDialogue("rota_1_manha");
            return;
        }

        if (resultado == "despertador_snooze")
        {
            AlterarHorario(2);
            DialogueManager.Instance.StartDialogue("rota_2_atrasado");
            
            return;
        }
    }

    public void AtivarEfeitoAnsiedade()
    {
        bool deveAtivar = Robin.ansiedade >= 7;

        // AnsiedadeManager.ansiedadeAlta = deveAtivar;

        if (canvasAnsiedade != null)
        {
            canvasAnsiedade.SetActive(deveAtivar);
        }
    }
}