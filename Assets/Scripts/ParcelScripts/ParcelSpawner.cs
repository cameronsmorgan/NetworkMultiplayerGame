using UnityEngine;
using Mirror;
public class ParcelSpawner : NetworkBehaviour
{
    [Header("Parcel Settings")]
    public GameObject[] parcelPrefabs; // Assign in inspector
    public Transform spawnPoint;
    public float spawnInterval = 5f;

    [Header("Force Settings")]
    public Vector2 ejectionForce = new Vector2(2f, 0f); // direction and strength of force applied 
    public ForceMode2D forceMode = ForceMode2D.Impulse;

    private float timer;

    public override void OnStartServer()   //timing to controlled by the server
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

    [Server]  //server controls spawning
    void SpawnRandomParcel()
    {
        if (parcelPrefabs.Length == 0) return;

        int index = Random.Range(0, parcelPrefabs.Length);
        GameObject parcelInstance = Instantiate(parcelPrefabs[index], spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(parcelInstance);  //ensures spawned object is visible across network

        // Apply force if Rigidbody2D exists
        Rigidbody2D rb = parcelInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(ejectionForce, forceMode);
        }
    }
}
