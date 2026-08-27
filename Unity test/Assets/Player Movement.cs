using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 movementInput;

    void Update()
    {
        // 1. Reset values each frame
        float horizontal = 0f;
        float vertical = 0f;

        // 2. Check if a keyboard is connected, then read keys directly
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal = 1f;
        }

        // 3. Store the clean directional vector
        movementInput = new Vector2(horizontal, vertical);

        // 4. Move the object smoothly across the screen
        transform.Translate(movementInput * moveSpeed * Time.deltaTime);
    }
}
