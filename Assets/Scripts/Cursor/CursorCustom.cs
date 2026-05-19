using UnityEngine;

public class CursorCustom : MonoBehaviour
{
    public Texture2D cursorTexture;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
    public void AtivarCursor()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void DesativarCursor()
    {
        // Reseta para o cursor padrão do sistema operacional
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
