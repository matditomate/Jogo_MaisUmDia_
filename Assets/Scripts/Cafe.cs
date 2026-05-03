using UnityEngine;

public enum CatState
{
    Hungry,
    Eating,
    Full,
    Locked
}

public class Cafe : MonoBehaviour
{
    public PanelCafeOpener minigamePanel;

    public CatState state = CatState.Hungry;

    void Update()
    {
        if (state == CatState.Locked)
            return;
    }

    public void AchouCafe()
    {
        if (state == CatState.Locked)
            return;

        if (state != CatState.Hungry)
            return;

        Debug.Log("Abriu minigame");

        state = CatState.Eating;
        minigamePanel.AbrirPanel();
    }

    public void SetFull()
    {
        state = CatState.Full;
        Debug.Log("Gato ficou satisfeito");
    }

    public void LockCat()
    {
        state = CatState.Locked;
        Debug.Log("Gato bloqueado permanentemente");
    }
}