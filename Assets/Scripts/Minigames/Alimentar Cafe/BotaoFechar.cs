using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    [Header("Referência do Jogo")]
    public Cafe cafe;

    public void FecharMinigame()
    {
        // Para a lógica de queda de prefabs imediatamente ao desistir
        RacaoDrop.jogoAtivo = false;

        // Deixa o PanelCafeOpener cuidar de toda a limpeza pesada
        if (cafe != null && cafe.minigamePanel != null)
        {
            cafe.minigamePanel.FecharPanel();
        }
        else
        {
            Debug.LogWarning("Minigame fechado, mas a referência do script Cafe sumiu!");
        }
    }
}