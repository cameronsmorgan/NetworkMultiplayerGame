using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CustomNetworkManager : NetworkManager
{
    [Header("Player Prefabs")]
    public List<GameObject> playerPrefabs = new List<GameObject>(); // Drag all your player prefabs here

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // Safety check
        if (playerPrefabs.Count == 0)
        {
            Debug.LogError("No players assigned!");
            return;
        }

        // int index = Random.Range(0, playerPrefabs.Count); //for assigning randomly

        // Example logic: assign based on player index
        int index = numPlayers % playerPrefabs.Count;

        GameObject selectedPrefab = playerPrefabs[index];

        GameObject player = Instantiate(selectedPrefab);

        PlayerRoleScript roleComponent = player.GetComponent<PlayerRoleScript>();
        if (roleComponent != null)
        {
            if (index == 0)
            {
                roleComponent.role = PlayerRoleScript.Role.Witch;
            }
            else
            {
                roleComponent.role = PlayerRoleScript.Role.Goblin;
            }
        }
        else
        {
            Debug.Log("Player prefab is missing in PlayerRoleScript");
        }
        NetworkServer.AddPlayerForConnection(conn, player);
    }

}
