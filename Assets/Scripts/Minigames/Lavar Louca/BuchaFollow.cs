using UnityEngine;

public class BuchaFollow : MonoBehaviour
{
    void Update()
    {
        // No Canvas (UI), usamos a posição direta do mouse
        transform.position = Input.mousePosition;
    }
}