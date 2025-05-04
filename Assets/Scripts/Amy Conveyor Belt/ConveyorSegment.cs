using System.Collections.Generic;
using UnityEngine;

public class ConveyorSegment : MonoBehaviour
{
    [Header("Assign exit and second points")]
    public Transform exitPoint;
    public Transform secondPoint;

    [Header("Connected conveyor segments")]
    public List<ConveyorSegment> nextSegments;

    public Transform spawnPoint;

    // Static toggle shared across all conveyors
    public static bool globalUseSecondPoint = false;
}

