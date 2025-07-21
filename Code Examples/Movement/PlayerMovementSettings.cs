using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "PlayerMovementSettings")]
public class PlayerMovementSettings : ScriptableObject
{

    [Header("Camera")]
    public float Sensitivity = 150f;
    public float MaxTiltAngle = 25f;
    public float CameraAngleClamp = 90f;
    public float CameraAngleTiltSpeed = 8f;

    [Header("Input")]
    public float InputTreshold = 0.1f;

    [Header("Movement")]
    public float AccelerationSpeed = 6300f;
    public float DragIndex = 5.25f;
    public float MaxWalkingVelocity = 15f;
    public float ThresholdVelocity = 0.1f;

    [Header("Running")]
    public float RunningAccelerationSpeed = 9450f;
    public float MaxRunningVelocity = 20f;

    [Header("Crouching")]
    public Vector3 CrouchingHeight = new(1f, 0.89f, 1f);
    public float CrouchingSpeed = 6f;
    public float CrouchThreshold = 0.05f;

    [Header("CounterMovement")]
    public float CrouchCounterMovement = 0.45f;
    public float CounterMovement = 0.075f;
    public float CounterForceWhenJumpingOfTheWall = 0.45f;
    public float AirCounterMovement = 0.17f;
    public float AirResistance = 580f;

    [Header("Sliding")]
    public float SlideForce = 3525f;
    public float SlideThreshold = 25f;
    public float SlidingLinearVelocity = 6f;
    public float SlidingMovementVectorTreshold = 0.65f;
    public float SlideForceLerp = 6f;


    [Header("Jumping")]
    public float JumpForce = 15f;
    public float JumpDirectionBoost = 0.5f;

    [Header("Gravity")]
    public float Gravity = 15f;

    [Header("Slope Settings")]
    public float MaxSlopeAngle = 45f;

    [Header("WallRunning")]
    public LayerMask Wall;
    public float WallrunningForce = 14000f;
    public float WallrunningForceUpwordMultiplier = 1.1f;
    public float WallrunningEndForce = 25f;
    public float WallrunningForceLerpSpeed = 4.5f;
    public float WallrunningForcePushBack = 39375f;
    public float WallrunningForceSideWayMultiplier = 0.65f;
    public float RangeToTheWall = 1.05f;

}
