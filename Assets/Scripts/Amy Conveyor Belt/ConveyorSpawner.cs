using Mirror;
using UnityEngine;

public class ConveyorSpawner : NetworkBehaviour
{
    public GameObject redPrefab;
    public GameObject bluePrefab;
    public GameObject yellowPrefab;

    public ConveyorSegment startingSegment; // initial spawn point for the first direction
    public ConveyorSegment secondarySegment; // new spawn point for toggled direction

    public float spawnInterval = 2f;
    private float timer = 0f;

    private bool isUsingSecondarySegment = false; // toggles spawn point direction

    void Update()
    {
        if (!isServer) return; // Only the server controls spawning

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnRandomObject();
        }
    }

    // Spawn object at the correct spawn point depending on toggle state
    void SpawnRandomObject()
    {
        int rand = Random.Range(0, 3);
        GameObject objToSpawn;

        if (rand == 0) objToSpawn = redPrefab;
        else if (rand == 1) objToSpawn = bluePrefab;
        else objToSpawn = yellowPrefab;

        GameObject instance = Instantiate(objToSpawn, startingSegment.spawnPoint.position, Quaternion.identity);
        NetworkServer.Spawn(instance);

        ConveyorMover mover = instance.GetComponent<ConveyorMover>();
        mover.StartMoving(startingSegment);
    }


    // Toggle the spawn direction
    public void ToggleSpawnDirection()
    {
        isUsingSecondarySegment = !isUsingSecondarySegment;
    }

    GameObject SelectRandomPrefab()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0) return redPrefab;
        else if (rand == 1) return bluePrefab;
        else return yellowPrefab;
    }

}


