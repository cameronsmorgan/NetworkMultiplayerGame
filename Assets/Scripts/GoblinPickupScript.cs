using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class GoblinPickupScript : NetworkBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float pickupRadius = 1f;
    [SerializeField] private LayerMask objectLayer;

    private GameObject grabbedObject;

    void Update()
    {
        if (!isLocalPlayer) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
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

        // Reset velocity to stop unintended motion
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Disable physics simulation while held
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;


        // Disable physics simulation while held
        target.transform.SetParent(grabPoint);
        target.transform.localPosition = Vector3.zero;

        grabbedObject = target;

        RpcAttachObject(target, netIdentity);
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


        grabbedObject.transform.SetParent(null);

        GameObject dropped = grabbedObject;
        grabbedObject = null;

        RpcDetachObject(dropped);
    }

    [ClientRpc]
    void RpcAttachObject(GameObject obj, NetworkIdentity parentId)
    {
        if (obj != null && parentId != null)
        {
            Transform grabTarget = parentId.transform.GetComponent<GoblinPickupScript>().grabPoint;
            obj.transform.SetParent(grabTarget);
            obj.transform.localPosition = Vector3.zero;
        }
    }

    [ClientRpc]
    void RpcDetachObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.SetParent(null);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
