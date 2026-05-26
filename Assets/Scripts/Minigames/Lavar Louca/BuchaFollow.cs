using UnityEngine;

public class BuxaFollow : MonoBehaviour
{
    void Update()
    {
        // No Canvas (UI), usamos a posição direta do mouse
        transform.position = Input.mousePosition;
    }
}