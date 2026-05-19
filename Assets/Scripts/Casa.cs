using UnityEngine;

public class Casa : MonoBehaviour
{
    public static int sujeira = 5; // 10 = muito suja


    public void AlterarSujeira(int valor)
    {
        sujeira = Mathf.Clamp(sujeira + valor, 0, 10);
        Debug.Log($"Sujeira da casa: {sujeira}");
    }

}