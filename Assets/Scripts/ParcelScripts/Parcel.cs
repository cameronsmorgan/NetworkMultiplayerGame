using UnityEngine;
using Mirror;
public class Parcel : NetworkBehaviour
{
    public enum ParcelType { Pink, Red, Blue }
    public ParcelType parcelType;
}
