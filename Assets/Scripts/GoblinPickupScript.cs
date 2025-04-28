using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System.Collections;

public class GoblinPickupScript : NetworkBehaviour
{
    [SerializeField] private Transform grabPosition;
    [SerializeField] private float pickupRadius = 1f;
    [SerializeField] private LayerMask objectLayer;

    [SyncVar] private GameObject grabbedObject; //  make this SyncVar to sync server/client

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
            CmdPickupObject(hit.gameObject);
        }
    }

    [Command]
    void CmdPickupObject(GameObject target)
    {
        if (grabbedObject != null || target == null) return;

        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;

        target.transform.SetParent(grabPosition);
        target.transform.localPosition = Vector3.zero;

        grabbedObject = target;

        RpcUpdateObjectPosition(target);
        Debug.Log($"[SERVER] Picked up object: {target.name}");
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

        // Send the grabbedObject to the Rpc BEFORE setting it null
        RpcClearObjectPosition(grabbedObject);

        grabbedObject.transform.SetParent(null);
        Debug.Log($"[SERVER] Dropping object: {grabbedObject.name}");

        grabbedObject = null;
    }

    [ClientRpc]
    void RpcUpdateObjectPosition(GameObject target)
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
