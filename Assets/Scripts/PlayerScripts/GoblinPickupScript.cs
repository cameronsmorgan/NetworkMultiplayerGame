using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System.Collections;

public class GoblinPickupScript : NetworkBehaviour
{
    [SerializeField] private Transform grabPosition;
    [SerializeField] private float pickupRadius = 1f;
    [SerializeField] private LayerMask objectLayer;

    [SyncVar] private GameObject grabbedObject; //Syncvar keeps the grabbed object synched across network

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (grabbedObject == null)
            {
                TryPickup();
            }
            else
            {
                CmdDropObject();
            }
        }
    }

    void TryPickup()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, pickupRadius, objectLayer);
        if (hit != null)
        {
            CmdPickupObject(hit.gameObject);    //if an object is found, the command function is called to handle pickup logic on the server
        }
    }

    [Command]
    void CmdPickupObject(GameObject target)
    {
        if (grabbedObject != null || target == null) return;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // Stop Conveyor
        ConveyorMover mover = target.GetComponent<ConveyorMover>();
        if (mover != null)
        {
            mover.PauseMovement();
        }

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;

        target.transform.SetParent(grabPosition);
        target.transform.localPosition = Vector3.zero;

        grabbedObject = target;

        RpcUpdateObjectPosition(target);
        Debug.Log($"[SERVER] Picked up object: {target.name}");

        Parcel parcel = target.GetComponent<Parcel>();
        if (parcel != null)
        {
            parcel.isPickedUp = true;
        }
    }


    [Command]
    void CmdDropObject()
    {
        if (grabbedObject == null) return;

        Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.simulated = true;
        }

        // Resume Conveyor
        ConveyorMover mover = grabbedObject.GetComponent<ConveyorMover>();
        if (mover != null)
        {
            mover.ResumeMovement();
        }

        RpcClearObjectPosition(grabbedObject);
        grabbedObject.transform.SetParent(null);

        Debug.Log($"[SERVER] Dropping object: {grabbedObject.name}");
        grabbedObject = null;
    }


    [ClientRpc]
    void RpcUpdateObjectPosition(GameObject target)    //executes on all clients
    {
        if (target != null)
        {
            target.transform.SetParent(grabPosition);
            target.transform.localPosition = Vector3.zero;
        }
    }

    [ClientRpc]
    void RpcClearObjectPosition(GameObject target)
    {
        if (target != null)
        {
            target.transform.SetParent(null);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
