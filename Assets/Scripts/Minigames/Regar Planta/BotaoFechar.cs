using UnityEngine;

public class MinigameUIPlanta : MonoBehaviour
{
    [Header("Configurações de UI")]
    public GameObject panelMinigame;

    [Header("Cursores")]
    public CursorCustom cursorMaoPadrao;

    public void FecharMinigame()
    {
        // DESCONGELA A CÂMERA E AS PORTAS!
        CameraPanLateral.minigameAtivo = false; 

        // Remove o cursor do regador e traz a mão padrão de volta
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        
        if (cursorMaoPadrao != null)
        {
            cursorMaoPadrao.AtivarCursor();
        }
        
        Cursor.visible = true;

        // Esconde o minigame
        if (panelMinigame != null)
        {
            panelMinigame.SetActive(false);
        }

        Debug.Log("[PLANTEI] Minigame de regar cancelado. Progresso descartado.");
    }
}