using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
    public float speed = 1.5f;
    private ConveyorSegment currentSegment;
    private Transform targetPoint;

    private bool isPaused = false;

    public bool useSecondPoint = false;

    public void StartMoving(ConveyorSegment startSegment)
    {
        currentSegment = startSegment;
        targetPoint = useSecondPoint ? currentSegment.secondPoint : currentSegment.exitPoint;
    }


    public void PauseMovement() => isPaused = true;

    public void ResumeMovement() => isPaused = false;

    void Update()
    {
        if (isPaused || currentSegment == null) return;

        UpdateTargetPoint(); // Dynamically refresh the target point

        if (targetPoint == null) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            if (currentSegment.nextSegments.Count > 0)
            {
                int rand = Random.Range(0, currentSegment.nextSegments.Count);
                currentSegment = currentSegment.nextSegments[rand];
                UpdateTargetPoint(); // Update target for new segment
            }
            else
            {
                Destroy(gameObject); // End of conveyor path
            }
        }
    }

    void UpdateTargetPoint()
    {
        targetPoint = ConveyorSegment.globalUseSecondPoint && currentSegment.secondPoint != null
            ? currentSegment.secondPoint
            : currentSegment.exitPoint;
    }

}


