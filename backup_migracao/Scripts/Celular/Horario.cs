using UnityEngine;
using TMPro;

public class Horario : MonoBehaviour
{
    private static string horarioTexto;
    private static string diaTexto;
    private static string[] diasSemana = new string[] { "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira"};
    [SerializeField] private TextMeshProUGUI horaIniTMPro;
    [SerializeField] private TextMeshProUGUI horaDesbloqTMPro;
    [SerializeField] private TextMeshProUGUI diaTMPro;

    void Start()
    {
        AlterarTextos();
    }

    void OnEnable()
    {
        GameManager.OnHorarioMudou += AlterarTextos;
    }

    void OnDisable()
    {
        GameManager.OnHorarioMudou -= AlterarTextos;
    }

    public void CalculoHorario()
    {
        float horario = GameManager.horario;
        int horas = (int)horario;
        int segundos = (int)((horario - horas) * 60f);
        Debug.Log(horario);
        Debug.Log(horas);
        Debug.Log(segundos);
        horarioTexto = string.Format("{0:D2}:{1:D2}", horas, segundos);
    }

    public void AlterarTextos()
    {
        CalculoHorario();
        horaIniTMPro.text = horarioTexto;
        horaDesbloqTMPro.text = horarioTexto;
        diaTexto = string.Format("{0}, {1}", diasSemana[GameManager.dia - 1], GameManager.dia);
        diaTMPro.text = diaTexto;
    }
}
