using UnityEngine;

public class TriggerPia : MonoBehaviour
{
    [SerializeField] private GameObject canvasMinigame;
    public static bool minigameBloqueado = false; // A flag que trava o minigame
    [SerializeField] private Texture2D cursorInteracao; // Arrastar a textura de lavar louça/interação nisso
    [SerializeField] private CursorCustom cursorMaoPadrao; // Opcional: Arrastar o objeto com o CursorCustom nisso
    private Vector2 hotspot = Vector2.zero;

    private void OnMouseEnter()
    {
        // Só muda o cursor se não estiver bloqueado e o minigame não estiver aberto na tela
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
        // Quando o mouse sai, volta para o cursor padrão (se o minigame não tiver aberto)
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

    void OnMouseDown()
    {
        // Só abre se não estiver bloqueado
        if (!minigameBloqueado && !CameraPanLateral.minigameAtivo)
        {
            CameraPanLateral.minigameAtivo = true; // congela a camera e portas!

            // Força o reset do cursor antes de sumir com ele
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            canvasMinigame.SetActive(true);
            Cursor.visible = false;
        }
        else
        {
            Debug.Log("A louça já está limpa!");
        }
    }

    // Função para ser chamada por outros eventos para liberar a pia novamente
    public void DesbloquearPia()
    {
        minigameBloqueado = false;
        Debug.Log("A pia está suja de novo!");
    }
}