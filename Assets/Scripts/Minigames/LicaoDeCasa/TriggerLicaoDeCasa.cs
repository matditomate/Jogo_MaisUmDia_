using UnityEngine;

public class TriggerLicaoDeCasa : MonoBehaviour
{
    [Header("Configurações de UI")]
    [SerializeField] private GameObject canvasMinigame;
    [SerializeField] private GameObject cafeDeitado;
    public static bool minigameBloqueado = false; // A flag que trava o minigame

    [Header("Cursores")]
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
            AbrirPanel();
        }
        else
        {
            Debug.Log("Lição de casa realizada!");
        }
    }

    public void AbrirPanel()
    {
        Debug.Log("[PANEL] Abrindo minigame");
        CameraPanLateral.minigameAtivo = true; // Congela camera e portas

        // if (cursorMaoPadrao != null)
        // {
        //     cursorMaoPadrao.DesativarCursor();
        // }

        // Libera o cursor do sistema operacional e remove o de interação
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;

        canvasMinigame.SetActive(true);
        cafeDeitado.SetActive(false);
        cursorMaoPadrao.AtivarCursor();
    }

    public void FecharPanel()
    {
        Debug.Log("[PANEL] Fechando minigame");
        CameraPanLateral.minigameAtivo = false; // Descongela camera e portas

        if (cursorMaoPadrao != null)
        {
            cursorMaoPadrao.AtivarCursor();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1f;

        canvasMinigame.SetActive(false);
        cafeDeitado.SetActive(true);
    }
}