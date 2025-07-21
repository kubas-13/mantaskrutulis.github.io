using UnityEngine;

[CreateAssetMenu(fileName = "NewLockpickingSettings", menuName = "LockpickingData ")]
public class LockpinkSettings : ScriptableObject
{
    [Header("Prefabs")]

    public LayerMask LootBox;

    [Header("Input")]
    public float AxisZInputThreshold = 0.1f;

    [Header("Durability Settings")]
    [Range(1f, 10f)]
    public float DurabilityByDifficultyDecrease = 1.5f;
    public float DurabilityAtEasiestDifficulty = 10f;
    public float DurabilityBreakSpeed = 2.25f;


    [Header("Sweet Spot Settings")]
    [Range(1f, 10f)]
    public float SweetSpotToleranceByDifficultyDecrease = 2.5f;
    public float SweetSpotToleranceAtEasiestDifficulty = 15f;

    [Header("Distance Calculations")]
    public float DistanceThreshold = 0.5f;
    public float DotProductThreshold = 0.9f;

    [Header("Angle Calculations")]
    public float MaximumAngleOfTheLock = 180f;
    public float MinimumAngleOfTheLock = 0f;
    public float RangeKoeficient = 30f;


    [Header("Other Settings")]
    public float LockPickingSpeed = 12f;
    public float RangeToLootBox = 3.5f;
    public float ShakeIntensity = 0.55f;

}
