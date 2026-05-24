using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPanLateral : MonoBehaviour
{
    public float speed = 20f;
    
    [Range(0f, 0.2f)]
    [Header("Tamanho da Borda (Ex: 0.05 = 5% da tela)")]
    public float edgePercentage = 0.05f; 
    
    [Header("Configurações de Limite")]
    public SpriteRenderer background;

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 pos = transform.position;

        // Calcula a posição do mouse de 0.0 (total esquerda) a 1.0 (total direita)
        float mouseXNormalizado = mousePosition.x / Screen.width;

        // Move para a Direita (Se o mouse estiver nos 5% finais da tela)
        if (mouseXNormalizado >= (1.0f - edgePercentage))
        {
            pos.x += speed * Time.deltaTime;
        }
        // Move para a Esquerda (Se o mouse estiver nos 5% iniciais da tela)
        else if (mouseXNormalizado <= edgePercentage)
        {
            pos.x -= speed * Time.deltaTime;
        }

        // Aplica os limites dinâmicos
        if (background != null)
        {
            float camHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
            
            float bgLeft = background.bounds.min.x;
            float bgRight = background.bounds.max.x;

            float minX = bgLeft + camHalfWidth;
            float maxX = bgRight - camHalfWidth;

            if (minX > maxX)
            {
                pos.x = background.bounds.center.x;
            }
            else
            {
                pos.x = Mathf.Clamp(pos.x, minX + 0.05f, maxX - 0.05f);
            }
        }

        transform.position = pos;
    }
}