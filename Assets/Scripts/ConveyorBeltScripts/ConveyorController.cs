using UnityEngine;
using Mirror;

public class ConveyorController : NetworkBehaviour
{
    public float interactionRange = 1.5f;
    public LayerMask conveyorLayer;

    void Update()
    {
        if (!isLocalPlayer) return;

        var role = GetComponent<PlayerRoleScript>();
        if (role == null || role.role != PlayerRoleScript.Role.Witch) return;   //checks for witch

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, interactionRange, conveyorLayer);
            if (hit != null)
            {
                var belt = hit.GetComponent<ConveyorBeltScript>();
                if (belt != null)
                {
                    belt.CmdFlipDirection();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
