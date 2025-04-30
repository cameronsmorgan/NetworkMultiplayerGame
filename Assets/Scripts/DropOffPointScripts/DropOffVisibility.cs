using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class DropOffVisibility : NetworkBehaviour
{
    [Header("the visual GameObject")]
    public GameObject visual;

    public void UpdateVisibility(bool isGoblin)
    {
        if (visual == null)
        {
            Debug.LogWarning("No visual assigned");
            return;
        }

        visual.SetActive(!isGoblin); // Hide if Goblin, Show if Witch
    }
}