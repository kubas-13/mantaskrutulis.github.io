using UnityEngine;

public class JumpingState : MovementBaseState
{  
    private LayerMask _wall;
    private Rigidbody _rb;
    private InputManager _input;
    private Transform _player;
    private Vector3 _up;
    private float _jumpForce;
    private float _inputThreshold;
    private float _rangeToWall;
    private bool _leftWall;
    private bool _rightWall;
    private bool _exitCollision;

    public override void SetupState(MovementStateManager MSM)
    {
        _player = MSM.transform;
        _rb = MSM.Rb;
        _wall = MSM.Prefab.Wall;
        _input = MSM.Input;
        _inputThreshold = MSM.Prefab.InputTreshold;
        _up = _player.transform.up;
        _jumpForce = MSM.Prefab.JumpForce;
        _rangeToWall = MSM.Prefab.RangeToTheWall;
    }
    public override void StartState(MovementStateManager MSM)
    {
        _exitCollision = false;
        _rb.linearVelocity = new(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        _rb.AddForce(_jumpForce * _up, ForceMode.Impulse);
        _rightWall = false;
        _leftWall = false;
        Debug.Log("Start jump State");

    }
    public override void UpdateState(MovementStateManager MSM)
    {
        ChooseYourWallSide(MSM);
    }
    public override void FixedUpdateState(MovementStateManager MSM){}
    public override void OnCollision(Collision collision, MovementStateManager MSM)
    {
        if (_exitCollision)
        {
            _exitCollision = false;
            MSM.SwitchingState(MSM.IdleState);
        }
    }
    public void Exit()
    {
        _exitCollision = true;
    }
#region wallRun
    private void ChooseYourWallSide(MovementStateManager MSM)
    {
        float x = _input.AxisX;

        for (int i = 0; i <= 1; i++)
        {
            Vector3 direction = (i == 0) ? _player.transform.right : -_player.transform.right;
            if (Physics.Raycast(_player.position, direction, _rangeToWall, _wall))
            {
                _rightWall = i == 0;
                _leftWall = i == 1;
                StartWallrun(_leftWall, _rightWall, x, MSM);
                return;
            }

        }
    }

    private void StartWallrun(bool leftWall, bool rightWall, float xAxis, MovementStateManager MSM)
    {
        if (leftWall && xAxis < -_inputThreshold)
        {
            MSM.Sign = -1;
            MSM.SwitchingState(MSM.WallrunState);

            return;
        }
        else if (rightWall && xAxis > _inputThreshold)
        {
            MSM.Sign = 1;
            MSM.SwitchingState(MSM.WallrunState);
        }
    }
    #endregion

}
