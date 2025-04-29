using UnityEngine;
using Mirror;
public class ParcelSpawner : NetworkBehaviour
{
    [Header("Parcel Settings")]
    public GameObject[] parcelPrefabs; // Assign in inspector
    public Transform spawnPoint;
    public float spawnInterval = 5f;

    private float timer;

    public override void OnStartServer()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        if (!isServer) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnRandomParcel();
            timer = spawnInterval;
        }
    }

    [Server]
    void SpawnRandomParcel()
    {
        if (parcelPrefabs.Length == 0) return;

        int index = Random.Range(0, parcelPrefabs.Length);
        GameObject parcelInstance = Instantiate(parcelPrefabs[index], spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(parcelInstance);
    }
}
