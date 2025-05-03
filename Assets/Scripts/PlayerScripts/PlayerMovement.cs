using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;

    public Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        if (movementInput.x != 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Sign(movementInput.x) * Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }

        //animation 
        bool isWalking = movementInput.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);

        Debug.Log("isWalking: " + isWalking);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;

        movementInput = context.ReadValue<Vector2>();
    }
}
