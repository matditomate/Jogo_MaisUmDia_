using UnityEngine;

public class Robin : MonoBehaviour
{
    // --- STATUS ESTÁTICOS (Persistem entre cenas) ---

    // Usamos float para permitir mudanças graduais (ex: diminuir energia aos poucos)
    public static float fome = 5f;      // 10 = Faminta , 0 = Sem fome
    public static float higiene = 5f;   // 10 = Imundo, 0 = Limpa
    public static float diversao = 5f;  // 10 = Não esta divertido, 0 = Muito Divertido
    public static float energia = 5f;   // 10 = Exausto, 0 = Disposta
    public static float ansiedade = 0f;    // 0 = Calma, 10 = Crise
    public static float progresso = 0f;    // 0 = Início, 10 = Meta (Depressão)

    // --- FUNÇÕES DE MANIPULAÇÃO ---

    public static void AlterarFome(float valor)
    {
        // Esse Math Clamp não deixa o valor passar do limite, que aqui é 10
        // Se somar e passar, devolve 10
        fome = Mathf.Clamp(fome + valor, 0, 10);
        Debug.Log($"Fome da Robin: {fome}");
    }

    public static void AlterarHigiene(float valor)
    {
        higiene = Mathf.Clamp(higiene + valor, 0, 10);
        Debug.Log($"Higiene da Robin: {higiene}");
    }

    public static void AlterarDiversao(float valor)
    {
        diversao = Mathf.Clamp(diversao + valor, 0, 10);
    }

    public static void AlterarEnergia(float valor)
    {
        energia = Mathf.Clamp(energia + valor, 0, 10);
    }

    public static void AlterarAnsiedade(float valor)
    {
        ansiedade = Mathf.Clamp(ansiedade + valor, 0, 10);
    }

    public static void AlterarProgresso(float valor)
    {
        progresso = Mathf.Clamp(progresso + valor, 0, 10);
        Debug.Log($"Progresso da Depressão: {progresso}");
    }
}