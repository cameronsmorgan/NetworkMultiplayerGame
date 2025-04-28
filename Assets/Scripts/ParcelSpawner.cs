using UnityEngine;
using Mirror;
public class ParcelSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject parcelPrefab; // Assign in Inspector
    [SerializeField] private Transform[] spawnPoints; // Optional multiple spawn points

    public override void OnStartServer()
    {
        base.OnStartServer();

        SpawnParcels();
    }

    [Server]
    void SpawnParcels()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject parcel = Instantiate(parcelPrefab, spawnPoint.position, Quaternion.identity);
            NetworkServer.Spawn(parcel);
        }
    }
}
