using System.Collections.Generic;
using UnityEngine;

public class ArrowStateManager : MonoBehaviour
{
    public ArrowSettings Prefab;

    public Transform Player;

    public float Velocity { get; set; }

    ArrowBaseState _currentState;
    public ArrowShootState ShootState = new();
    public ArrowIdleState IdleState = new();
    public CalculationForArrowVelocity CalculationState = new();
    private List<ArrowBaseState> _allState;
  
    void Start()
    {
        _allState = new List<ArrowBaseState>()
        {
        ShootState,
        IdleState,
        CalculationState
        };
        foreach (var state in _allState)
        {
            state.SetupState(this);
        }
        _currentState = IdleState;
        _currentState.StartState(this);
    }    
    public void SwitchState(ArrowBaseState ABS)
    {
        _currentState = ABS;
        ABS.StartState(this);
    }
}
