using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class DropOffVisibility : NetworkBehaviour
{
    [Header("Assign the visual GameObject (e.g., the sprite holder) here")]
    public GameObject visual;

    public void UpdateVisibility(bool isGoblin)
    {
        if (visual == null)
        {
            Debug.LogWarning($"No visual assigned on {gameObject.name}");
            return;
        }

        visual.SetActive(!isGoblin); // Hide if Goblin, Show if Witch
    }
}