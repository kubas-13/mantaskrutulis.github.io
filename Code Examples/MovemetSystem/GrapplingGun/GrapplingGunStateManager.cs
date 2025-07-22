using System.Collections.Generic;
using UnityEngine;


public class GrapplingGunStateManager : MonoBehaviour
{

    [Header("Prefab")]
    public GrapplingGunSettings GrappleSettings; 
    public Rigidbody Rb { get; set; }
    public MovementStateManager Msm { get; set; }
    public LineRenderer Lr { get; set; }
    public InputManager Input { get; private set; } 
    
    public GameObject Pointer;
    public Transform Camera;   

    [Header("Settings")] 
    public Vector3 SavedClosestPoint { get; set; }
    public float SpeedR { get; set; }
   

    [Header("States")]
    GrapplingGunBaseState _currentState;
    public GrapplingGunGetReadyForGrapple _grappleToPoint = new();
    public GrapplingGunGrapplePoint _grappleGetPoint = new();
    private List<GrapplingGunBaseState> _allState;
    private void Awake()
    { 
        Rb = GetComponent<Rigidbody>();
        Msm = GetComponent<MovementStateManager>();
        Lr = GetComponent<LineRenderer>();
        Input = FindAnyObjectByType<InputManager>();
        _allState = new List<GrapplingGunBaseState>()
        {
            _grappleToPoint,
            _grappleGetPoint
        };
        foreach (var state in _allState)
        {
            state.SetupState(this);
        }

        if (Input == null)
        {
            Debug.LogError("NOOO InputManager in the scene");
        }
    }
    

    void Start()
    {
        _currentState = _grappleGetPoint;
        _currentState.StartState(this);
    }

    void Update()
    {
        _currentState.UpdateState(this);
        Lr.SetPosition(0, transform.position);

    }
    public void SwitchState(GrapplingGunBaseState state)
    {
        _currentState = state;
        state.StartState(this);
    }
}
