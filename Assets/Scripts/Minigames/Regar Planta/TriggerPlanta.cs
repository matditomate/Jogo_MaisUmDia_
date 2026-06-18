using UnityEngine;

public class TriggerPlanta : MonoBehaviour
{
    [SerializeField] private GameObject canvasMinigame;
    [SerializeField] private GameObject fundoSala;
    [SerializeField] private GameObject fundoSacada;
    
    public static bool minigameBloqueado = false;
    
    [SerializeField] private Texture2D cursorInteracao; // Arrastar textura do hover nisso
    [SerializeField] private CursorCustom cursorMaoPadrao; // Opcional: script de cursor padrão
    
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
            fundoSacada.SetActive(false);
            fundoSala.SetActive(false);
            Cursor.visible = false;
            
            // congela camera e portas, variavel que criei na camera
            CameraPanLateral.minigameAtivo = true;
            Debug.Log("Câmera e portas congeladas para o minigame.");
        }
    }

    public void FecharMinigameRegar()
    {
        canvasMinigame.SetActive(false);
        fundoSacada.SetActive(true);
        fundoSala.SetActive(true);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        Cursor.visible = true;
        
        // libera camera e porta no final
        CameraPanLateral.minigameAtivo = false;
        Debug.Log("Câmera e portas liberadas.");
    }

    void OnMouseDown()
    {
        // Só abre o minigame da planta se ela não estiver bloqueada 
        // E se nenhumoutro minigame estiver aberto na tela!
        if (!minigameBloqueado && !CameraPanLateral.minigameAtivo)
        {
            AbrirMinigameRegar();
        }
    }
}