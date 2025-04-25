using UnityEngine;

public class ConveyorBeltScript : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right;
    public float speed = 2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.linearVelocity = moveDirection.normalized * speed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.linearVelocity = Vector2.zero;
        }
    }
}
