using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 mundoPos = Camera.main.ScreenToWorldPoint(mousePos);

            RaycastHit2D hit = Physics2D.Raycast(mundoPos, Vector2.zero);

            if(hit.collider != null)
            {
                Debug.Log("Clicou em: " + hit.collider.name);

                if(hit.collider.TryGetComponent<ObjetoInterativo>(out ObjetoInterativo interativo))
                {
                    interativo.AoClicar();
                }
            }
        }
    }
}
