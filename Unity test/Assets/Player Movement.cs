using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    private float horizontalInput;

    [Header("Jumping")]
    public float jumpForce = 12f; // Increase this value in the Inspector to jump HIGHER!
    public Transform groundCheck;  // An empty GameObject placed at the player's feet
    public LayerMask groundLayer;  // Set this to your "Ground" layer in Unity
    private bool isGrounded;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Horizontal Movement (Left/Right)
        horizontalInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontalInput = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontalInput = 1f;
        }

        // 2. Ground Check (Prevents infinite jumping in mid-air)
        // Checks if a small imaginary circle at the player's feet overlaps with the Ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // 3. Jump Input
        // If the player presses W, Space, or Up, AND they are standing on the ground
        if ((Keyboard.current.wKey.wasPressedThisFrame || 
             Keyboard.current.spaceKey.wasPressedThisFrame || 
             Keyboard.current.upArrowKey.wasPressedThisFrame) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Move horizontally while preserving whatever vertical speed (falling/jumping) gravity dictates
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    // Draw the ground check circle in the editor so you can see it
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
}
