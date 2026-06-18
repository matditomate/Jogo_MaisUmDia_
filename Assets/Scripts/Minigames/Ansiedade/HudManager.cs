using UnityEngine;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;

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
            return;
        }
    }
}
