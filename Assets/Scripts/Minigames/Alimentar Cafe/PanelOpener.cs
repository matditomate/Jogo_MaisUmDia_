using UnityEngine;

public class PanelCafeOpener : MonoBehaviour
{
    [Header("Configurações de Objetos")]
    // Removi a desativação do worldRoot para que ele continue visível ao fundo
    public GameObject worldRoot; 
    public GameObject minigamePanel;

    [Header("Componentes de Interface")]
    public CanvasGroup canvasGroup;
    public ScoopController scoop;

    [Header("Referência do Jogo")]
    public Cafe cafe;

    public void AbrirPanel()
    {
        Debug.Log("[PANEL] Abrindo minigame");

        // Liberar o cursor para interagir com o Canvas
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Opcional: Pausa o tempo do jogo (física, timers, etc)
        // Time.timeScale = 0f; 

        // Ativamos o painel do minigame
        minigamePanel.SetActive(true);

        // Configuramos o CanvasGroup para ser visível e interativo
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        // Configuração do objeto "Scoop" (concha/colher)
        if (scoop != null)
        {
            scoop.transform.SetParent(minigamePanel.transform, false);
            scoop.transform.SetAsLastSibling();
            scoop.gameObject.SetActive(true);
            scoop.minigameAtivo = true;
            scoop.cheio = false;
            scoop.enabled = true; // Garante que o script do scoop funcione
        }
    }

    public void FecharPanel()
    {
        Debug.Log("[PANEL] Fechando minigame");

        // Retorna o cursor ao estado normal do seu jogo (ex: travado para FPS)
        // Mude para CursorLockMode.Locked se for um jogo em primeira pessoa
        Cursor.visible = true; 
        Cursor.lockState = CursorLockMode.None;

        // Retorna o tempo ao normal
        Time.timeScale = 1f;

        if (scoop != null)
        {
            scoop.minigameAtivo = false;
            scoop.cheio = false;
            scoop.enabled = false;
        }

        // Esconde o painel
        minigamePanel.SetActive(false);

        // Desativa a interação com o CanvasGroup
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}