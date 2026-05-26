using UnityEngine;

public class TriggerPlanta : MonoBehaviour
{
    [SerializeField] private GameObject canvasMinigame;
    
    public static bool minigameBloqueado = false;
    
    [SerializeField] private Texture2D cursorInteracao; // Arraste a textura do hover aqui
    [SerializeField] private CursorCustom cursorMaoPadrao; // Opcional: Seu script de cursor padrão
    
    private Vector2 hotspot = Vector2.zero;

    private void OnMouseEnter()
    {
        // SÓ MUDA O CURSOR SE: não estiver bloqueado E a câmera não estiver travada em nenhum minigame
        if (!minigameBloqueado && !CameraPanLateral.minigameAtivo)
        {
            if (cursorInteracao != null)
            {
                Cursor.SetCursor(cursorInteracao, hotspot, CursorMode.Auto);
            }
        }
    }

    private void OnMouseExit()
    {
        // Só tenta restaurar o cursor se o minigame NÃO estiver rodando na tela
        if (!CameraPanLateral.minigameAtivo)
        {
            if (cursorMaoPadrao != null)
            {
                cursorMaoPadrao.AtivarCursor();
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }

    public void AbrirMinigameRegar()
    {
        if (!minigameBloqueado)
        {
            canvasMinigame.SetActive(true);
            Cursor.visible = false;
            
            // CONGELA A CÂMERA E AS PORTAS (Usa a variável que criamos na câmera)
            CameraPanLateral.minigameAtivo = true;
            Debug.Log("Câmera e portas congeladas para o minigame.");
        }
    }

    public void FecharMinigameRegar()
    {
        canvasMinigame.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        Cursor.visible = true;
        
        // LIBERA A CÂMERA E AS PORTAS NO FINAL
        CameraPanLateral.minigameAtivo = false;
        Debug.Log("Câmera e portas liberadas.");
    }

    void OnMouseDown()
    {
        AbrirMinigameRegar();
    }
}