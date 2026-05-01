using UnityEngine;
using UnityEngine.Events;

public class ObjetoInterativo : MonoBehaviour
{
    [Header("O que acontece ao clicar?")]
    public UnityEvent acaoAoClicar;

    public void AoClicar()
    {
        acaoAoClicar.Invoke();
    }
}
