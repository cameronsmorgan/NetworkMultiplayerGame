using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class DropOffPointScript : NetworkBehaviour
{
    public Parcel.ParcelType acceptedParcelType;
    public int pointValue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isServer) return;   //runs ONLY on server 

        Parcel parcel = collision.gameObject.GetComponent<Parcel>();   //gets parcel component from collided game object
        if (parcel != null && parcel.parcelType == acceptedParcelType)
        {
            
            ScoreManager.Instance.AddPoints(pointValue);

            
            NetworkServer.Destroy(parcel.gameObject);  //destroys the parcel across network
        }
    }
}
