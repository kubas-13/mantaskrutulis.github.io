using UnityEngine;

public class CrouchingState : MovementBaseState
{
    private Rigidbody _rb;
    private InputManager _input;
    private Transform _player;
    private Vector3 _crouchingHeight;
    private float _crouchThreshold;
    private float _crouchingSpeed;
    private float _crouchingCounterMovement;
    private float _acceleration;
    private bool _ceilingIsOnTop;
    public override void SetupState(MovementStateManager MSM)
    {
        _crouchingCounterMovement = MSM.Prefab.CrouchCounterMovement;
        _crouchingHeight = MSM.Prefab.CrouchingHeight;
        _player = MSM.transform;
        _rb = MSM.Rb;
        _input = MSM.Input;
        _crouchingSpeed = MSM.Prefab.CrouchingSpeed;
        MSM.MaxVelocity = MSM.Prefab.MaxWalkingVelocity;
        _acceleration = MSM.Prefab.AccelerationSpeed;
        _crouchThreshold = MSM.Prefab.CrouchThreshold;

    }
    public override void StartState(MovementStateManager MSM) { Debug.Log("Start Crouch State");
        MSM.Acceleration = _acceleration;
    }
    public override void UpdateState(MovementStateManager MSM)
    {
        CheckForInputs(MSM);    
    }
    public override void FixedUpdateState(MovementStateManager MSM)
    {
        SmoothCrouchingDown(_crouchingHeight);
        ActiveForcesInCrouch(MSM);
    }
    public override void OnCollision(Collision collision, MovementStateManager MSM)
    {
    }
    private bool CheckForCeiling()
    {
       bool ceiling = Physics.Raycast(_player.transform.position, _player.transform.up, 2.5f);
        return ceiling;
    }
    private void CheckForInputs(MovementStateManager MSM)
    {
        _ceilingIsOnTop = CheckForCeiling();
        if (_input.IsRunning && !_ceilingIsOnTop)
        {
            MSM.SwitchingState(MSM.RunState);
        }
        if (!_input.Crouch && !_ceilingIsOnTop)
        {
            MSM.SwitchingState(MSM.IdleState);
        }   
    }
    private void ActiveForcesInCrouch(MovementStateManager MSM)
    {
        Vector3 movementVector = MSM.MovementVector.normalized;
        _rb.AddForce(_acceleration * _crouchingCounterMovement * Time.deltaTime * -movementVector, ForceMode.Force);
    }

    private void SmoothCrouchingDown(Vector3 crouchHeight)
    {
        if (Vector3.Distance(_player.localScale, crouchHeight) < _crouchThreshold)
        {
            return;
        }

        _player.localScale = Vector3.Lerp(_player.localScale, crouchHeight, _crouchingSpeed * Time.deltaTime);
    }
}
