using UnityEngine;

public class WallrunningState : MovementBaseState
{
    private LayerMask _wall;
    private Rigidbody _rb;
    private InputManager _input;
    private Transform _player;  
    private int _sign;
    private float _wallrunningForce;
    private float _wallrunningForceLerpSpeed;
    private float _wallrunningForceLerpEnd;
    private float _wallrunningForcePushBack;
    private float _wallrunningForceSideWayMultiplier;
    private float _forceMultiplier;
    private float _rangeToTheWall;
    private float _savedWallrunningForce;
    private float _cameraAngleTiltSpeed;
    private float _cameraMaxTiltAngle;
    private bool _isNearWall;
    public override void SetupState(MovementStateManager MSM)
    {
        _savedWallrunningForce = MSM.Prefab.WallrunningForce;
        _wallrunningForceLerpEnd = MSM.Prefab.WallrunningEndForce;
        _wallrunningForceLerpSpeed = MSM.Prefab.WallrunningForceLerpSpeed;
        _cameraAngleTiltSpeed = MSM.Prefab.CameraAngleTiltSpeed;
        _cameraMaxTiltAngle = MSM.Prefab.MaxTiltAngle;
        _wall = MSM.Prefab.Wall;
        _wallrunningForceSideWayMultiplier = MSM.Prefab.WallrunningForceSideWayMultiplier;
        _rangeToTheWall = MSM.Prefab.RangeToTheWall;
        _forceMultiplier = MSM.Prefab.WallrunningForceUpwordMultiplier;
        _wallrunningForcePushBack = MSM.Prefab.WallrunningForcePushBack;
        _input = MSM.Input;
        _rb = MSM.Rb;
        _player = MSM.transform;
    }

    public override void StartState(MovementStateManager MSM)
    {
        _wallrunningForce = _savedWallrunningForce;
        _sign = MSM.Sign;
        _rb.linearVelocity = new(_rb.linearVelocity.x / 1.25f, Mathf.Abs(_rb.linearVelocity.y) / 4, _rb.linearVelocity.z / 1.25f);
        Debug.Log("Start WallRun State");

    }
    public override void UpdateState(MovementStateManager MSM)
    {
        ChooseYourWallSide(MSM);
        CheckForInput(MSM);
    }
    private void CheckForInput(MovementStateManager MSM)
    {
        if (_input.Jumped)
        {
            JumpOffTheWall(_sign, MSM);
        }
    }
    public override void FixedUpdateState(MovementStateManager MSM)
    {
        Wallrunning(_sign, MSM);
    }
    public override void OnCollision(Collision collision, MovementStateManager MSM)
    {
        MSM.SwitchingState(MSM.IdleState);
    }
    private void ChooseYourWallSide(MovementStateManager MSM)
    {

        for (int i = 0; i <= 1; i++)
        {
            Vector3 direction = (i == 0) ? _player.transform.right : -_player.transform.right;
            if (Physics.Raycast(_player.position, direction, _rangeToTheWall, _wall))
            {
                _isNearWall = true;
                return;
            }
            _isNearWall = false;
        }
        if (!_isNearWall)
        {
            MSM.SwitchingState(MSM.IdleState);
        }
    }

    private void CameraZAxisTilt(int sign, MovementStateManager MSM)
    {
        MSM.CameraAngleAxisZ = Mathf.Lerp(MSM.CameraAngleAxisZ, _cameraMaxTiltAngle * sign, _cameraAngleTiltSpeed * Time.deltaTime);
    }

    private void Wallrunning(int sign, MovementStateManager MSM)
    {
        _wallrunningForce = Mathf.Lerp(_wallrunningForce, _wallrunningForceLerpEnd, _wallrunningForceLerpSpeed * Time.deltaTime);
        _rb.AddForce(_wallrunningForce * Time.deltaTime * MSM.transform.forward, ForceMode.Force);
        _rb.AddForce(_wallrunningForce * _forceMultiplier * Time.deltaTime * MSM.transform.up, ForceMode.Force);
        _rb.AddForce(sign * _wallrunningForce * _wallrunningForceSideWayMultiplier * Time.deltaTime * MSM.transform.right, ForceMode.Force);
        CameraZAxisTilt(sign, MSM);
    }
    public void JumpOffTheWall(int sign, MovementStateManager MSM)
    {
        Vector3 velocityOnWallrun = MSM.Prefab.CounterForceWhenJumpingOfTheWall * -new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);

        _rb.linearVelocity += velocityOnWallrun;
        _rb.AddForce(-sign * _wallrunningForcePushBack * Time.deltaTime * _player.transform.right, ForceMode.Impulse);
        MSM.SwitchingState(MSM.JumpState);
    }

}
