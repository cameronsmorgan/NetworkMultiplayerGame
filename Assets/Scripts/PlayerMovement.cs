using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }

    // Hook this up to the "Move" UnityEvent in PlayerInput
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;

        movementInput = context.ReadValue<Vector2>();
    }
}
