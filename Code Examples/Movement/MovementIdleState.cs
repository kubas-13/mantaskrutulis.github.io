using UnityEngine;

public class MovementIdleState : MovementBaseState
{
    private InputManager _input;
    private Transform _player;
    private Vector3 _startHeight;   
    private float _crouchThreshold;
    private float _crouchingSpeed;

    public override void SetupState(MovementStateManager MSM)
    {
        _crouchThreshold = MSM.Prefab.CrouchThreshold;
        _crouchingSpeed = MSM.Prefab.CrouchingSpeed;
        _input = MSM.Input;
        _player = MSM.transform;
        _startHeight = _player.transform.localScale;
    }
    public override void StartState(MovementStateManager MSM)
    {
        MSM.MaxVelocity = MSM.Prefab.MaxWalkingVelocity;
        MSM.Acceleration = MSM.Prefab.AccelerationSpeed;
        Debug.Log("Start Idle State");

    }
    public override void UpdateState(MovementStateManager MSM)
    {       
        CheckForInput(MSM);
    }
    public override void FixedUpdateState(MovementStateManager MSM)
    {
     SmoothCrouchingUp(_startHeight);
    }
    public override void OnCollision(Collision collision, MovementStateManager MSM)
    { }

    private void CheckForInput(MovementStateManager MSM)
    {
        if (_input.Running)
        {
            MSM.SwitchingState(MSM.RunState);
        }
        if (_input.Jumped && MSM.OnGround)
        {
            MSM.SwitchingState(MSM.JumpState);
        }
        if (_input.Crouch && MSM.OnGround)
        {
            MSM.SwitchingState(MSM.CrouchState);
        }
    }

    private void SmoothCrouchingUp(Vector3 startHeight)
    {
        if (Vector3.Distance(_player.localScale, startHeight) < _crouchThreshold)
        {
            return;
        }

        _player.localScale = Vector3.Lerp(_player.localScale, startHeight, _crouchingSpeed * Time.deltaTime);
    }
}
