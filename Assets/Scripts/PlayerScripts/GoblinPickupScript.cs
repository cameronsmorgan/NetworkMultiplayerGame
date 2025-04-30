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

        rb.bodyType = RigidbodyType2D.Kinematic;    //turns the object's physics off to prevent falling and other physics issues
        rb.simulated = false;

        target.transform.SetParent(grabPosition);  //sets the parent to the grabPosition object
        target.transform.localPosition = Vector3.zero;  //positions the object at the grabPosition

        grabbedObject = target;

        RpcUpdateObjectPosition(target);     //informs all clients to update the objects position
        Debug.Log($"[SERVER] Picked up object: {target.name}");
    }

    [Command]
    void CmdDropObject()
    {
        if (grabbedObject == null) return;

        Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;      //re-enables the objects physics
            rb.simulated = true;
        }

                  
        RpcClearObjectPosition(grabbedObject);    //tells clients to update the objects position

        grabbedObject.transform.SetParent(null);  //removes the parent
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
