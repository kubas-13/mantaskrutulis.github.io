using UnityEngine;

public class CalculationForArrowVelocity : ArrowBaseState
{
    private Transform _arrow;
    private Transform _player;
    private Rigidbody _rb;
    private Rigidbody _playerRb;
    private float _angle;
    private float _gravity;
    private float _velocity;
    private int _timesForCalutations;
    private float distanceBetweenVectors = 0;
    private float distanceBetween = 0;
    private float time = 0;
    public override void SetupState(ArrowStateManager ASM)
    {
        _angle = Mathf.Deg2Rad * ASM.Prefab.Angle;
        _timesForCalutations = ASM.Prefab.HowManyTimesForCalucation;
        _rb = ASM.GetComponent<Rigidbody>();
        _gravity = Physics.gravity.y;
        _arrow = ASM.transform;
        _player = ASM.Player;
        _playerRb = _player.gameObject.GetComponent<Rigidbody>();
    }
    public override void StartState(ArrowStateManager ASM)
    {
        Calculate(ASM);
        Debug.Log("Start Calculation State");

    }
    void Calculate(ArrowStateManager ASM)
    {

        float sinAngle = Mathf.Sin(2 * _angle);
        for (int i = 0; i < _timesForCalutations; i++)
        {
            if (i == 0)
            {
                FirstCalculation(i);
            }
            else
            {
                OtherCalculation(i);
            }

        }      
        _arrow.LookAt(_player.position + time * _playerRb.GetPointVelocity(_arrow.position)); 
        _velocity = Mathf.Sqrt(Mathf.Abs(distanceBetween * _gravity / sinAngle));
        if (_rb.linearVelocity.magnitude < 0.1f)
        {
            ASM.Velocity = _velocity;
            ASM.SwitchState(ASM.ShootState);
        }

    }
    private void FirstCalculation(int index)
    {
        if (index != 0)
        {
            return;
        }
        distanceBetweenVectors = Vector3.Distance(_arrow.position, _player.position);
        time = Mathf.Sqrt(Mathf.Abs(2 * distanceBetweenVectors * Mathf.Tan(_angle) / _gravity));
        distanceBetween = Vector3.Distance(_arrow.position, _player.position + time * _playerRb.GetPointVelocity(_arrow.position));
    }
    private void OtherCalculation(int index)
    {
        if (index == 0)
        {
            return;
        }
        time = Mathf.Sqrt(Mathf.Abs(2 * distanceBetween * Mathf.Tan(_angle) / _gravity));
        distanceBetween = Vector3.Distance(_arrow.position, _player.position + time * _playerRb.GetPointVelocity(_arrow.position));
    }
}
