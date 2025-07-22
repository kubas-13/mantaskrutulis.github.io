using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "GrapplingGun", menuName = "GrapplingGunSettings" )]
public class GrapplingGunSettings : ScriptableObject
{
    public LayerMask grapplePoint;
    public int ColliderCountInTheList = 25;
    public float Speed = 2.25f;
    public float LenghtOfSquare = 10;
    public float JumpForceForTheEdge = 28f;
    public float DistanceThresholdOfGrapplingPoint = 1.5f;
}
