using UnityEngine;
using Mirror;

public class ConveyorBeltScript : NetworkBehaviour
{
    public Vector2 moveDirection = Vector2.right;

    public float speed = 2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isServer) return; // Make sure logic runs on server only

        // Check if object is a parcel by tag or component
        if (other.CompareTag("Parcel") && other.attachedRigidbody != null)
        {
            other.attachedRigidbody.linearVelocity = moveDirection.normalized * speed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isServer) return;

        if (other.CompareTag("Parcel") && other.attachedRigidbody != null)
        {
            other.attachedRigidbody.linearVelocity = Vector2.zero;
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdFlipDirection()
    {
        moveDirection *= -1;
    }
}
