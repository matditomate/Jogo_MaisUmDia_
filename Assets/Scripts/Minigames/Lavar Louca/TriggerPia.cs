using UnityEngine;

public class TriggerPia : MonoBehaviour
{
    [SerializeField] private GameObject canvasMinigame;
    public static bool minigameBloqueado = false; // A flag que trava o minigame

    void OnMouseDown()
    {
        // Só abre se NÃO estiver bloqueado
        if (!minigameBloqueado)
        {
            canvasMinigame.SetActive(true);
            Cursor.visible = false;
        }
        else
        {
            Debug.Log("A louça já está limpa!");
        }
    }

    // Função para ser chamada por outros eventos para liberar a pia novamente
    public void DesbloquearPia()
    {
        minigameBloqueado = false;
        Debug.Log("A pia está suja de novo!");
    }
}