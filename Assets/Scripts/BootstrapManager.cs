using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    [Header("Prefabs globais")]
    [SerializeField] private GameObject dialogueSystemPrefab;

    [Header("Primeira cena do jogo")]
    [SerializeField] private string firstSceneName = "Menu";

    private void Awake()
    {
        if (DialogueManager.Instance == null)
        {
            Instantiate(dialogueSystemPrefab);
        }

        SceneManager.LoadScene(firstSceneName);
    }
}