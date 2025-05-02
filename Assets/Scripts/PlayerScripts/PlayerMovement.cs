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

        if (movementInput.sqrMagnitude > 0.01f)
        {
            if (movementInput.x != 0)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = Mathf.Sign(movementInput.x) * Mathf.Abs(newScale.x);
                transform.localScale = newScale;
            }
        }
    }

    // Hook this up to the "Move" UnityEvent in PlayerInput
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;

        movementInput = context.ReadValue<Vector2>();
    }
}
