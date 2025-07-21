using UnityEngine;

public class GrapplingGunGetReadyForGrapple : GrapplingGunBaseState
{
    private Rigidbody _rb;
    private MovementStateManager _msm;
    private LineRenderer _lr;
    private Transform _player;
    private Vector3 _lerpAxis;
    private Vector3 _savedClosestPoint;
    private float _speedR;
    private float _speed;
    private float _jumpForce;
    private float _distanceThresholdOfGrapplingPoint;

    public override void SetupState(GrapplingGunStateManager GGSM)
    { 
        _player = GGSM.transform;
        _speed = GGSM.GrappleSettings.Speed;
        _jumpForce = GGSM.GrappleSettings.JumpForceForTheEdge;
        _distanceThresholdOfGrapplingPoint = GGSM.GrappleSettings.DistanceThresholdOfGrapplingPoint;
        _lr = GGSM.Lr;
        _msm = GGSM.Msm;
        _rb = GGSM.Rb;
    }
    public override void StartState(GrapplingGunStateManager GGSM)
    {
        _savedClosestPoint = GGSM.SavedClosestPoint;   
        _speedR = _speed / 3;
        _rb.linearVelocity = Vector3.zero;
        _msm.enabled = false;
        _lerpAxis = GGSM.transform.position;
    }
    public override void UpdateState(GrapplingGunStateManager GGSM)
    {

        float t = Time.deltaTime;
        _lerpAxis.x = Mathf.Lerp(_lerpAxis.x, _savedClosestPoint.x, _speed * t);
        _lerpAxis.y = Mathf.Lerp(_lerpAxis.y, _savedClosestPoint.y, _speedR * t);
        _lerpAxis.z = Mathf.Lerp(_lerpAxis.z, _savedClosestPoint.z, _speed * t);
        _speedR = Mathf.Lerp(_speedR, _speed * 1.75f, _speed * t);

        _lr.SetPosition(1, _savedClosestPoint);
        _player.transform.position = _lerpAxis;
        if (Vector3.Distance(_player.position, _savedClosestPoint) <= _distanceThresholdOfGrapplingPoint)
        {
            _msm.enabled = true;
            _rb.AddForce(_jumpForce * GGSM.transform.up, ForceMode.Impulse);
            GGSM.SwitchState(GGSM._grappleGetPoint);

        }
    }
}
