using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPanLateral : MonoBehaviour
{
    public float speed = 20f;
    public float edgeSize = 10f;
    
    [Header("Configurações de Limite")]
    public SpriteRenderer background;
    private float minX;
    private float maxX;

    void Start()
    {
        if (background != null)
        {
            CalcularLimites();
        }
    }

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 pos = transform.position;

        if (mousePosition.x >= Screen.width - edgeSize)
        {
            pos.x += speed * Time.deltaTime;
        }
        
        if (mousePosition.x <= edgeSize)
        {
            pos.x -= speed * Time.deltaTime;
        }

        if (background != null)
        {
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
        }

        transform.position = pos;
    }

    void CalcularLimites()
    {
        float camHalfWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        float bgLeft = background.bounds.min.x;
        float bgRight = background.bounds.max.x;

        minX = bgLeft + camHalfWidth;
        maxX = bgRight - camHalfWidth;
    }
}