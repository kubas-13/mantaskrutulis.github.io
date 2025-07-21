using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    [Header("Prefabs")]
    public PlayerMovementSettings Prefab;
    public Rigidbody Rb { get; set; }
    public InputManager Input { get; set; }
    
    public Transform Camera;
    [Header("Camera")]
    public Vector3 MovementVector{ get; set; }  
    public int Sign { get; set; }
    public float CameraAngleAxisZ { get; set; }
    private float _axisX;
    private float _axisY;
    public float MaxVelocity { get; set; }
    public float Acceleration { get; set; }
    public float InputThreshold { get; set; }
    public bool OnGround { get; set; }


    [Header("States")]
    MovementBaseState _currentState;
    public MovementIdleState IdleState = new();
    public RunningState RunState = new();
    public CrouchingState CrouchState = new();
    public JumpingState JumpState = new();
    public SlidingState SlideState = new();
    public WallrunningState WallrunState = new();

    private List<MovementBaseState> allStates;

    void Start()
    {
        Input = FindAnyObjectByType<InputManager>();
        Rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        InputThreshold = Prefab.InputTreshold;
        if (Input == null)
        {
            Debug.LogError("NOOO InputManager in the scene");
        }
        allStates = new List<MovementBaseState>()
        {
            IdleState,
        RunState,
        CrouchState,
        JumpState,
        SlideState,
        WallrunState
        };
    
        foreach (var state in allStates)
        {
            state.SetupState(this);
        }
        _currentState = IdleState;
        _currentState.StartState(this);

    }
    private void Update()
    {
        _currentState.UpdateState(this);
        CameraMovement(); 
        CameraZAxisTilt(0);
    }
    void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
        AirCounterMovement();
        Gravity();
        ObjectsMovement();
    }

    public void SwitchingState(MovementBaseState state)
    {
        _currentState = state;
        state.StartState(this);
    }

    #region Camera
    private void CameraMovement()
    {

        float cameraAxisX = Input.CameraAxisX;
        float cameraAxisY = Input.CameraAxisY;

        float y = cameraAxisX * Time.deltaTime * Prefab.Sensitivity;
        float x = cameraAxisY * Time.deltaTime * Prefab.Sensitivity;

        _axisY += y;
        _axisX -= x;

        _axisX = Mathf.Clamp(_axisX, -Prefab.CameraAngleClamp, Prefab.CameraAngleClamp);
        Camera.transform.rotation = Quaternion.Euler(_axisX, _axisY, CameraAngleAxisZ);

        transform.rotation = Quaternion.Euler(0, _axisY, 0);

    }
    #endregion
    #region Basic Movement

    protected void ObjectsMovement()
    {
        if (_currentState == WallrunState)
        { return;  }
        Vector3 velocity = transform.InverseTransformVector(Rb.linearVelocity);

        float x = Input.AxisX;
        float z = Input.AxisZ;

        if (Mathf.Abs(velocity.x) >= MaxVelocity)
        {
            x = 0;
        }
        if (Mathf.Abs(velocity.z) >= MaxVelocity)
        {
            z = 0;
        }

        MovementVector = (transform.forward * z) + (x * transform.right);
        Debug.Log(MovementVector.normalized.magnitude);
        Rb.AddForce(Acceleration * Time.deltaTime * MovementVector.normalized, ForceMode.Force);
        CounterMovement(velocity);
    }
    private void CameraZAxisTilt(int sign)
    {
        if (_currentState == WallrunState)
        {
            return;
        }
        CameraAngleAxisZ = Mathf.Lerp(CameraAngleAxisZ, sign, Prefab.CameraAngleTiltSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        float _angle = Mathf.Cos(Prefab.MaxSlopeAngle * Mathf.Deg2Rad);
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            if (normal.y >= _angle)
            {
                OnGround = true;
                _currentState.OnCollision(collision, this);
                Rb.linearDamping = Prefab.DragIndex;

            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        JumpState.Exit();
        OnGround = false;
        Rb.linearDamping = 0;
    }

    private void AirCounterMovement()
    {
        if (OnGround)
        { return; }
        Vector3 velocity = new(Rb.linearVelocity.x, 0, Rb.linearVelocity.z);
        if (Mathf.Abs(velocity.magnitude) > 2)
        {
            Rb.AddForce(Prefab.AirResistance * Prefab.AirCounterMovement * Time.deltaTime * -velocity, ForceMode.Force);
            Rb.AddForce(velocity.magnitude * Prefab.AirCounterMovement * -transform.up, ForceMode.Force);
        }
    }
    private void CounterMovement(Vector3 velocity)
    {
        if (!OnGround || _currentState == SlideState)
        { return; }
        
        if (Mathf.Abs(velocity.x) > Prefab.ThresholdVelocity && Mathf.Abs(Input.AxisX) < InputThreshold)
        {
            Rb.AddForce(Acceleration * Prefab.CounterMovement * Time.deltaTime * -velocity.x * transform.right, ForceMode.Force);
        }
        if (Mathf.Abs(velocity.z) > Prefab.ThresholdVelocity && Mathf.Abs(Input.AxisZ) < InputThreshold)
        {
            Rb.AddForce(Acceleration * Prefab.CounterMovement * Time.deltaTime * -velocity.z * transform.forward, ForceMode.Force);
        }
    }

    private void Gravity()
    {
        if (OnGround)
        { return; }
        Rb.AddForce(-transform.up * Prefab.Gravity, ForceMode.Force);
    }


    #endregion

}
