using UnityEngine;

public class MinigameUILouca : MonoBehaviour
{
    [Header("Configurações de UI")]
    public GameObject canvasMinigame;

    [Header("Cursores")]
    public CursorCustom cursorMaoPadrao;

    public void FecharMinigame()
    {
        // Descongela camera e portas
        CameraPanLateral.minigameAtivo = false; 

        // Remove o cursor de interação e traz o padrão de volta
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        
        if (cursorMaoPadrao != null)
        {
            cursorMaoPadrao.AtivarCursor();
        }
        
        Cursor.visible = true;

        // Desativa sem aplicar debuffs ao Robin ou mudar horário
        if (canvasMinigame != null)
        {
            canvasMinigame.SetActive(false);
        }

        Debug.Log("Minigame da Pia cancelado pelo jogador. Status preservados.");
    }
}