using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    public GameObject minigamePanel;

    public void FecharMinigame()
    {
        Time.timeScale = 1f;
        minigamePanel.SetActive(false);
    }
}