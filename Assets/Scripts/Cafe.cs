// TIVE QUE REFATORAR AQUI TAMBÉM!

using UnityEngine;


// Cafe = parte da CENA (cursor, mouse, minigame, miado).
// Os status (saciedade, atenção, locked) ficam no DataManager / GameManager.StatusCafe.

public class Cafe : MonoBehaviour
{
    public PanelCafeOpener minigamePanel;

    [Header("Sons do Minigame")]
    public AudioClip somMiado;
    [SerializeField] private AudioSource fonteMiado;

    [Header("Miado aleatório")]
    [Tooltip("Intervalo em SEGUNDOS (mínimo, máximo) entre um miado e outro.")]
    [SerializeField] private Vector2 intervaloMiado = new Vector2(60f, 270f);
    private float proximoMiado;

    [Header("Cursor")]
    [SerializeField] private Texture2D cursorInteracao; // Textura do mouse de carinho/interação
    private Vector2 hotspot = Vector2.zero;

    private DataManager Data => DataManager.instance;

    private void Start()
    {
        AgendarProximoMiado();
    }

    private void Update()
    {
        // Miado aleatório baseado em tempo (independente do FPS)
        if (Time.time >= proximoMiado)
        {
            Miar();
            AgendarProximoMiado();
        }
    }

    private void AgendarProximoMiado()
    {
        proximoMiado = Time.time + Random.Range(intervaloMiado.x, intervaloMiado.y);
    }

    private void Miar()
    {
        if (fonteMiado == null)
        {
            Debug.LogWarning("Cafe: fonteMiado não foi atribuída no Inspector.");
            return;
        }

        fonteMiado.Play();
    }

    private void OnMouseEnter()
    {
        // Só muda o cursor se o minigame NÃO estiver aberto e o gato NÃO estiver travado
        if (!Data.cafeLocked && minigamePanel != null && !minigamePanel.minigamePanel.activeSelf)
        {
            if (cursorInteracao != null)
            {
                Cursor.SetCursor(cursorInteracao, hotspot, CursorMode.Auto);
            }
        }
    }

    private void OnMouseExit()
    {
        // Se o minigame abriu, o PanelCafeOpener gerencia o mouse; aqui só restauramos se o painel estiver fechado
        if (minigamePanel != null && !minigamePanel.minigamePanel.activeSelf)
        {
            if (minigamePanel.CursorMao != null)
            {
                minigamePanel.CursorMao.AtivarCursor();
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }

    public void AchouCafe()
    {
        // Se estiver travado, nem tenta checar o resto
        if (Data.cafeLocked)
        {
            Debug.Log("Café está ignorando você completamente.");
            Miar();
            return;
        }

        // Saciedade < 10 = Café tem alguma fome (antes: fome > 0)
        if (Data.saciedadeCafe < 10)
        {
            Debug.Log("Café está com fome! Abrindo minigame.");

            // Força o reset do cursor imediatamente antes de abrir o painel
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            minigamePanel.AbrirPanel();
        }
        else
        {
            Debug.Log("Café não está com fome agora.");
        }
    }
}