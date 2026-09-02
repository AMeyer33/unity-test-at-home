using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    private float horizontalInput;

    [Header("Jumping")]
    public float jumpForce = 12f;
    public float fallMultiplier = 10f;     // Increases gravity when falling for a faster descent
    public float lowJumpMultiplier = 2f;    // Increases gravity if jump button is released early
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool jumpRequested;
    private bool isHoldingJump;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = 0f;

        if (Keyboard.current != null)
        {
            // Horizontal Input
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
                horizontalInput = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
                horizontalInput = 1f;

            // Track if jump key is held down
            isHoldingJump = Keyboard.current.wKey.isPressed || 
                            Keyboard.current.spaceKey.isPressed || 
                            Keyboard.current.upArrowKey.isPressed;

            // Jump Request
            if (Keyboard.current.wKey.wasPressedThisFrame || 
                Keyboard.current.spaceKey.wasPressedThisFrame || 
                Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                jumpRequested = true;
            }
        }
    }

    void FixedUpdate()
    {
        // Ground Check
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        // Apply Horizontal Movement
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Apply Jump
        if (jumpRequested)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            jumpRequested = false;
        }

        // --- SNAPPY JUMP PHYSICS ---
        // 1. Fall faster after reaching the peak of the jump
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        // 2. Short hop: Fall faster if the player releases the jump key mid-air
        else if (rb.linearVelocity.y > 0 && !isHoldingJump)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
}