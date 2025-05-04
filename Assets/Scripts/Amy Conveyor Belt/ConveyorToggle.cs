using UnityEngine;

public class ConveyorToggle : MonoBehaviour
{
    public ConveyorSpawner leftSpawner;  // Assign in Inspector
    public ConveyorSpawner rightSpawner; // Assign in Inspector

    private void OnMouseDown()
    {
        // When the conveyor is clicked, toggle both spawners
        leftSpawner.ToggleSpawnDirection();
        rightSpawner.ToggleSpawnDirection();
        Debug.Log("Toggled spawners via conveyor click!");
    }
}


