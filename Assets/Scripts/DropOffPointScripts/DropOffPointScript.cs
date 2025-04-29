using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

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
            // Award points
            ScoreManager.Instance.AddPoints(pointValue);

            // Destroy parcel
            NetworkServer.Destroy(parcel.gameObject);
        }
    }
}
