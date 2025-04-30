using UnityEngine;
using Mirror;

public class VisibilityManager : NetworkBehaviour
{

    public GameObject coloredVisual;
    public GameObject grayVisual;

    public override void OnStartClient()
    {
        base.OnStartClient();

        var player = NetworkClient.localPlayer.GetComponent<PlayerRoleScript>();   //gets the player role script of the current player

        if (player != null && player.role == PlayerRoleScript.Role.Witch)
        {
            coloredVisual.SetActive(true);
            grayVisual.SetActive(false);
        }
        else
        {
            coloredVisual.SetActive(false);
            grayVisual.SetActive(true);
        }
    }
}
