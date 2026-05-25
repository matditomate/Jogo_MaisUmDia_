using UnityEngine;

public class Casa : MonoBehaviour
{
    public static int sujeira = 5; // 10 = muito suja

    public static int aguaPlanta = 6;
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

}