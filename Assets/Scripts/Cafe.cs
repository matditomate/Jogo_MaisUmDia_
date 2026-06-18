using UnityEngine;

public class Cafe : MonoBehaviour
{
    public PanelCafeOpener minigamePanel;

    [Header("Sons do Minigame")]
    public AudioClip somMiado;
    [SerializeField] private AudioSource fonteMiado;

    [SerializeField] private Texture2D cursorInteracao; // Arrastar a textura do mouse de carinho/interação aqui
    private Vector2 hotspot = Vector2.zero;

    // --- STATUS ESTÁTICOS (Persistem entre cenas) ---
    public static int fome = 5;      // 0 a 10
    public static int atencao = 5;   // 0 a 10
    public static bool locked = false; // Trava total de interação

    

    void Update()
    {
        // Fazer gato Miar aleatoriamente
        int numero1 = Random.Range(1, 10001);
        int numero2 = Random.Range(1, 10001);

        if (numero1 == numero2)
        {
            fonteMiado.Play();
        }
    }

    private void OnMouseEnter()
    {
        // Só muda o cursor se o minigame NÃO estiver aberto e o gato NÃO estiver travado
        if (!locked && minigamePanel != null && !minigamePanel.minigamePanel.activeSelf)
        {
            if (cursorInteracao != null)
            {
                Cursor.SetCursor(cursorInteracao, hotspot, CursorMode.Auto);
            }
        }
    }

    private void OnMouseExit()
    {
        // Quando o mouse sai de cima do gato, volta para o cursor padrão
        // Se o minigame abriu, o PanelCafeOpener já vai gerenciar o mouse, então aqui só limpamos se o painel estiver fechado
        if (minigamePanel != null && !minigamePanel.minigamePanel.activeSelf)
        {
            // Se você tiver o CursorCustom na cena, podemos chamá-lo para restaurar a "mão" padrão:
            if (minigamePanel.CursorMao != null)
            {
                minigamePanel.CursorMao.AtivarCursor();
            }
            else
            {
                // Caso contrário, volta para o ponteiro padrão do sistema
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }
    public void AchouCafe()
    {
        // Se estiver travado (bool), nem tenta checar o resto
        if (locked)
        {
            Debug.Log("Café está ignorando você completamente.");
            fonteMiado.Play();
            return;
        }

        // Exemplo: Se a fome estiver baixa, abre o minigame de comida
        if (fome > 0)
        {
            Debug.Log("Café está com fome! Abrindo minigame.");

            // Força o reset do cursor imediatamente antes de abrir o painel
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

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