using UnityEngine;
using System;

public class Casa : MonoBehaviour
{
    public static Casa instancia;
    public static int sujeira = 5; // 10 = muito suja

    public static int aguaPlanta = 6;

    public static float horario = 6.0f;

    public static int dia = 1;

    private Horario horarioScript;

    public static event Action OnHorarioMudou;

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

    public void AlterarHoraio(float valor)
    {
        horario += valor;
        if(horario > 23.98f)
        {
            horario -= 23.98f;
            dia += 1;
        }

        if (OnHorarioMudou != null)
        {
            OnHorarioMudou.Invoke();
        }
    }

}