using UnityEngine;
using Mirror;

public class PlayerRoleScript : NetworkBehaviour
{

    public enum Role { Witch, Goblin }
    public Role role;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Find all DropOffVisibility objects in the scene
        DropOffVisibility[] dropOffPoints = FindObjectsOfType<DropOffVisibility>();

        bool isGoblin = role == Role.Goblin;

        foreach (var dropOff in dropOffPoints)
        {
            dropOff.UpdateVisibility(isGoblin);
        }
    }
}
