using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string portaDeDestino;

    void Awake()
    {
        // Garante que só exista um GameManager no jogo
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}