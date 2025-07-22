using UnityEngine;

public class RunningState : MovementBaseState
{ 
    private InputManager _input;
    private Transform _player;
    private Vector3 _startHeight;
    private float _crouchThreshold;
    private float _crouchingSpeed;
    private float _runningAccelerationSpeed;

    public override void SetupState(MovementStateManager MSM)
    {
        _crouchThreshold = MSM.Prefab.CrouchThreshold;
        MSM.MaxVelocity = MSM.Prefab.MaxRunningVelocity;
        _crouchingSpeed = MSM.Prefab.CrouchingSpeed;
        _player = MSM.transform;
        _startHeight = _player.transform.localScale;
        _input = MSM.Input;
        _runningAccelerationSpeed = MSM.Prefab.RunningAccelerationSpeed;
        Debug.Log("ready");

    }
    public override void StartState(MovementStateManager MSM)
    {
        MSM.Acceleration = _runningAccelerationSpeed;
        Debug.Log("Start Run State");
    }
    public override void UpdateState(MovementStateManager MSM)
    {     
        CheckForInputs(MSM);
    }
    public override void FixedUpdateState(MovementStateManager MSM)
    {
        SmoothCrouchingUp(_startHeight);
    }
    public override void OnCollision(Collision collision, MovementStateManager MSM){ }
    private void CheckForInputs(MovementStateManager MSM)
    {
        if (!_input.Running)
        {
            MSM.SwitchingState(MSM.IdleState);
            return;
        }
        if (_input.IsCrouching)
        {
            MSM.SwitchingState(MSM.SlideState);
            return;
        }
        if (_input.Jumped)
        {
            MSM.SwitchingState(MSM.JumpState);
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
