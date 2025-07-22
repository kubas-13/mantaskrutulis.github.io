using UnityEngine;

[CreateAssetMenu(fileName = "ArrowSettings", menuName = "ArrowSettings")]

public class ArrowSettings : ScriptableObject
{
    public float Angle = 30f;
    public float AttackRate = 2.5f;
    public int HowManyTimesForCalucation = 5;
}
