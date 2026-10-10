using UnityEngine;

public class TriggerLicaoDeCasa : MonoBehaviour
{
    [Header("Configurações de UI")]
    [SerializeField] private GameObject canvasMinigame;
    [SerializeField] private GameObject cafeDeitado;
    public static bool minigameBloqueado = false;

    [Header("Cursores")]
    [SerializeField] private Texture2D cursorInteracao; 
    [SerializeField] private CursorCustom cursorMaoPadrao; 
    private Vector2 hotspot = Vector2.zero;

    [Header("Sons do Minigame")]
    public AudioClip somEstudos; 

    private void OnMouseEnter()
    {
        if (!minigameBloqueado && !CameraPanLateral.minigameAtivo && cursorInteracao != null)
            Cursor.SetCursor(cursorInteracao, hotspot, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        if (!CameraPanLateral.minigameAtivo)
        {
            if (cursorMaoPadrao != null) cursorMaoPadrao.AtivarCursor();
            else Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    void OnMouseDown()
    {
        if (!minigameBloqueado && !CameraPanLateral.minigameAtivo)
        {
            AbrirPanel();
        }
    }

    public void AbrirPanel()
    {
        CameraPanLateral.minigameAtivo = true; 
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;

        if (canvasMinigame != null) canvasMinigame.SetActive(true);
        if (cafeDeitado != null) cafeDeitado.SetActive(false);
        if (cursorMaoPadrao != null) cursorMaoPadrao.AtivarCursor();

        // Inicia o som de estudos assim que o painel do minigame abrir
        if (somEstudos != null)
        {
            AudioManager.instance.TocarLooping(somEstudos);
        }
    }

    public void FecharPanel()
    {
        CameraPanLateral.minigameAtivo = false; 

        if (cursorMaoPadrao != null) cursorMaoPadrao.AtivarCursor();
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (canvasMinigame != null) canvasMinigame.SetActive(false);
        if (cafeDeitado != null) cafeDeitado.SetActive(true);

        // Para o som de estudos
        if (somEstudos != null && AudioManager.instance != null)
        {
            AudioManager.instance.PararLooping();
        }
    }
}