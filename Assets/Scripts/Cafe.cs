using UnityEngine;

public class Cafe : MonoBehaviour
{
    public PanelCafeOpener minigamePanel;

    // --- STATUS ESTÁTICOS (Persistem entre cenas) ---
    public static int fome = 5;      // 0 a 10
    public static int atencao = 5;   // 0 a 10
    public static bool locked = false; // Trava total de interação

    public void AchouCafe()
    {
        // Se estiver travado (bool), nem tenta checar o resto
        if (locked)
        {
            Debug.Log("Café está ignorando você completamente.");
            return;
        }

        // Exemplo: Se a fome estiver baixa, abre o minigame de comida
        if (fome > 0)
        {
            Debug.Log("Café está com fome! Abrindo minigame.");
            minigamePanel.AbrirPanel();
        }
        else
        {
            Debug.Log("Café não está com tanta fome agora.");
        }
    }

    // --- FUNÇÕES PARA ALTERAR OS STATUS (Chame de qualquer script) ---

    public static void AlterarFome(int valor)
    {
        fome += valor;
        fome = Mathf.Clamp(fome, 0, 10); // Garante que fique entre 0 e 10
        Debug.Log("Fome do Café agora é: " + fome);
    }

    // Mathf.Clamp(atencao, 0, 10); isso garante que não passe do maximo (10)
    // Se somar e passar de 10, devolve o maximo, que aqui é 10.
    public static void AlterarAtencao(int valor)
    {
        atencao += valor;
        atencao = Mathf.Clamp(atencao, 0, 10);
        Debug.Log("Atenção do Café agora é: " + atencao);
    }

    public static void SetLocked(bool estado)
    {
        locked = estado;
        Debug.Log("Estado Locked do Café: " + locked);
    }
}