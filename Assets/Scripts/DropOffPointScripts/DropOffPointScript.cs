using UnityEngine;
using Mirror;

public class DropOffPointScript : NetworkBehaviour
{
    public Parcel.ParcelType acceptedParcelType;
    public int pointValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isServer) return;

        Parcel parcel = collision.gameObject.GetComponent<Parcel>();
        if (parcel != null && parcel.parcelType == acceptedParcelType)
        {
            // Just in case ConveyorMover is still trying to move it
            ConveyorMover mover = parcel.GetComponent<ConveyorMover>();
            if (mover != null)
            {
                mover.PauseMovement(); // stops motion immediately
            }

            ScoreManager.Instance.AddPoints(pointValue);
            NetworkServer.Destroy(parcel.gameObject);
        }
    }
}

