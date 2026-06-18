using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems; // Required for UI events

public class ObjetoInterativo : MonoBehaviour, IPointerClickHandler
{
    [Header("O que acontece ao clicar?")]
    public UnityEvent acaoAoClicar;

    // This function is automatically called by Unity when the UI element is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        AoClicar();
    }

    public void AoClicar()
    {
        if (acaoAoClicar != null)
        {
            acaoAoClicar.Invoke();
        }
    }
}