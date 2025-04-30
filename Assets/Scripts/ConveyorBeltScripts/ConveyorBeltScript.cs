using UnityEngine;
using Mirror;

public class ConveyorBeltScript : NetworkBehaviour
{
    public Vector2 moveDirection = Vector2.right;

    public float speed = 2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isServer) return; //  ensures logic runs on server only

        
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
            other.attachedRigidbody.linearVelocity = Vector2.zero;    //velocity of the object resets when removed from trigger 
        }
    }

    [Command(requiresAuthority = false)]   //allows any client to run this command
    public void CmdFlipDirection()
    {
        moveDirection *= -1;      //flips direction of conveyor
    }
}
