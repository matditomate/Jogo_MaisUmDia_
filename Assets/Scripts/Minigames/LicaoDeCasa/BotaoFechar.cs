using UnityEngine;

public class BotaoFechar : MonoBehaviour
{
    [Header("Configurações de UI")]
    public GameObject canvasMinigame;

    [Header("Cursores")]
    public CursorCustom cursorMaoPadrao;

    [SerializeField] private GameObject cafeDeitado;

    [Header("Sons do Minigame")]
    public AudioClip somEstudos; 

    public void FecharMinigame()
    {
        Debug.Log("Clicou em sair");
        cafeDeitado.SetActive(true);
        // Descongela camera e portas
        CameraPanLateral.minigameAtivo = false; 

        // Remove o cursor de interação e traz o padrão de volta
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        
        if (cursorMaoPadrao != null)
        {
            cursorMaoPadrao.AtivarCursor();
        }
        
        Cursor.visible = true;

        if (somEstudos != null && AudioManager.instance != null)
        {
            AudioManager.instance.PararLooping();
        }

        // Desativa sem aplicar debuffs ao Robin ou mudar horário
        if (canvasMinigame != null)
        {
            canvasMinigame.SetActive(false);
        }

        Debug.Log("Minigame da Lição de casa fechado");
    }
}