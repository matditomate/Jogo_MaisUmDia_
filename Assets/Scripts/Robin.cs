using UnityEngine;

public class Robin : MonoBehaviour
{
    // --- STATUS ESTÁTICOS (Persistem entre cenas) ---

    
    public static int fome = 4;      // 10 = Faminta , 0 = Sem fome
    public static int higiene = 6;   // 10 = Limpo, 0 = Imundo
    public static int diversao = 7;  // 10 = Feliz, 0 = Entediado
    public static int energia = 8;   // 10 = Disposto, 0 = Exausto
    public static int ansiedade = 10;    // 0 = Calma, 10 = Crise
    public static int progresso = 0;    // 0 = Início, 10 = Meta (Depressão)

    private static GameManager gerenciador;

    void Awake()
    {
        gerenciador = Object.FindAnyObjectByType<GameManager>();
    }

    // --- FUNÇÕES DE MANIPULAÇÃO ---

    public static void AlterarFome(int valor)
    {
        // Esse Math Clamp não deixa o valor passar do limite, que aqui é 10
        // Se somar e passar, devolve 10
        fome = Mathf.Clamp(fome + valor, 0, 10);
        Debug.Log($"Fome de Robin: {fome}");
    }

    public static void AlterarHigiene(int valor)
    {
        higiene = Mathf.Clamp(higiene + valor, 0, 10);
        Debug.Log($"Higiene de Robin: {higiene}");
    }

    public static void AlterarDiversao(int valor)
    {
        diversao = Mathf.Clamp(diversao + valor, 0, 10);
        Debug.Log($"Diversão de Robin: {diversao}");
    }

    public static void AlterarEnergia(int valor)
    {
        energia = Mathf.Clamp(energia + valor, 0, 10);
        Debug.Log($"Energia de Robin: {energia}");
    }

    public static void AlterarAnsiedade(int valor)
    {
        ansiedade = Mathf.Clamp(ansiedade + valor, 0, 10);
        Debug.Log($"Ansiedade de Robin: {ansiedade}");
        gerenciador.AtivarEfeitoAnsiedade();
    }

    public static void AlterarProgresso(int valor)
    {
        progresso = Mathf.Clamp(progresso + valor, 0, 10);
        Debug.Log($"Progresso de Robin: {progresso}");
    }
    
}