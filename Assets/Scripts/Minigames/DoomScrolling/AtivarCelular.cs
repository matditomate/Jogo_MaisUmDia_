using UnityEngine;

public class AtivarCelular : MonoBehaviour
{
    [Header("UI Reference")]
    [Tooltip("Arraste o CanvasDoomScrolling para cá")]
    [SerializeField] private GameObject canvasCelular;

    private void OnMouseDown()
    {
        // Quando o jogador clica no objeto do cenário, ativa o minigame
        if (canvasCelular != null)
        {
            // CameraPanLateral.minigameAtivo = true;
            canvasCelular.SetActive(true);
        }
    }
}