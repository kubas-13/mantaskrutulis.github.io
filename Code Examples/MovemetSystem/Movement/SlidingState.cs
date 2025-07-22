using UnityEngine;

public class SlidingState : MovementBaseState
{
    private Rigidbody _rb;
    private Transform _player;
    private Vector3 _crouchHeight;
    private float _crouchThreshold;
    private float _crouchingSpeed;
    private float _slideForce;
    private float _slideForceLerp;
    private float _slideThreshold;
    private float _slideLinearVelocityThreshold;
    private float _slideMovementVectorThreshold;
    private float _savedSlideForce;
    public override void SetupState(MovementStateManager MSM)
    {
        _savedSlideForce = MSM.Prefab.SlideForce;
        _slideThreshold = MSM.Prefab.SlideThreshold;
        _rb = MSM.Rb;
        _slideForceLerp = MSM.Prefab.SlideForceLerp;
        _slideLinearVelocityThreshold = MSM.Prefab.SlidingLinearVelocity;
        _slideMovementVectorThreshold = MSM.Prefab.SlidingMovementVectorTreshold;
        _crouchHeight = MSM.Prefab.CrouchingHeight;
        _player = MSM.transform;
        _crouchingSpeed = MSM.Prefab.CrouchingSpeed;
        _crouchThreshold = MSM.Prefab.CrouchThreshold;

    }
    public override void StartState(MovementStateManager MSM)
    {
        _slideForce = _savedSlideForce;
        Debug.Log("Start Slide State");

    }
    public override void UpdateState(MovementStateManager MSM){  }
    public override void FixedUpdateState(MovementStateManager MSM)
    {
         Sliding(MSM);
        SmoothCrouchingDown(_crouchHeight);
    }
    public override void OnCollision(Collision collision, MovementStateManager MSM){ }
    private void Sliding(MovementStateManager MSM)
    {
        Vector3 movementVector = MSM.MovementVector;
        if (movementVector.magnitude < _slideMovementVectorThreshold)
        {
            movementVector = _slideMovementVectorThreshold * _player.transform.forward;
        }
        _slideForce = Mathf.Lerp(_slideForce, 0, _slideForceLerp * Time.deltaTime);
        float linearVelocity = _rb.linearVelocity.magnitude;
        if (linearVelocity <= _slideLinearVelocityThreshold)
        {
            linearVelocity = _slideLinearVelocityThreshold;
        }
        _rb.AddForce(_slideForce * linearVelocity * Time.deltaTime * movementVector.normalized, ForceMode.Force);
        if (_slideForce < _slideThreshold)
        {
            MSM.SwitchingState(MSM.CrouchState);
        }
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
