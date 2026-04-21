using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private float vertical;

    private bool isFacingRight = true;
    private bool jumpPressed;
    private bool jumpReleased;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        // Movimento horizontal (A/D ou seta)
        horizontal = Keyboard.current.aKey.isPressed ? -1 :
                     Keyboard.current.dKey.isPressed ? 1 : 0;

        // Detectar pulo (pressionar)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpPressed = true;
        }


        vertical = 0; //não deixa voando infinito
        if (Keyboard.current.wKey.isPressed) vertical = 1;
        if (Keyboard.current.sKey.isPressed) vertical = -1;

        // Detectar soltar pulo
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            jumpReleased = true;
        }

        if (jumpPressed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            jumpPressed = false;
        }

        if (jumpReleased && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            jumpReleased = false;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        //Pulo com SPACEBAR
        // rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Movimento com W / S
        rb.linearVelocity = new Vector2(horizontal * speed, vertical * speed);
        

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}