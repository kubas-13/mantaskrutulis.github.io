using UnityEngine;

public class LockpickingState : LockpickingIdleState
{
    private Transform _lockpickBigger;
    private Transform _lockpickSmaller;
    private Animator _animator;
    private Quaternion _lockpinkAngle;
    private float _maxAngle;
    private float _minAngle;
    private float _inputThreshold;
    private float _middlePoint;
    private float _StartAngleOfLockpick;
    private float _rangeKoeficient;
    private float _completionRate;
    private float _successRate;
    private float _minZRot = 0f;
    private float _maxZRot = 0f;
    private float _angle = 0f;
    private float _shakeIntensity = 0f;
    private float _durabilityBreakSpeed = 0f;
    private float _durability;
    private float _lockPickingSpeed = 0f;
    

    public override void SetupState(LockpickingStateManager LMS)
    {
        _lockpickBigger = LMS.LockpickNumber1;
        _lockpickSmaller = LMS.LockpickNumber2;
        _rangeKoeficient = LMS.Prefab.RangeKoeficient;
        _lockPickingSpeed = LMS.Prefab.LockPickingSpeed;
        _durabilityBreakSpeed = LMS.Prefab.DurabilityBreakSpeed;
        _shakeIntensity = LMS.Prefab.ShakeIntensity;
        float _sweerSpot = LMS.SweetSpotTolerance;
        _maxAngle = LMS.Prefab.MaximumAngleOfTheLock;
        _minAngle = LMS.Prefab.MinimumAngleOfTheLock;
        _middlePoint = Random.Range(_sweerSpot, _maxAngle - _sweerSpot);
        _minZRot = _middlePoint - _sweerSpot;
        _maxZRot = _middlePoint + _sweerSpot;
        _inputThreshold = LMS.Prefab.AxisZInputThreshold;
    }
    public override void StartState(LockpickingStateManager LMS)
    {   
        _animator = LMS.LootBox.GetComponent<Animator>();
        _durability = LMS.LockpicksDurability;
        _angle = GetAngleMidpoint(_minAngle, _maxAngle); 
        _lockpinkAngle = Quaternion.Euler(_lockpickBigger.transform.localRotation.eulerAngles);
        _StartAngleOfLockpick = _lockpinkAngle.eulerAngles.x - _angle;
    }
    float GetAngleMidpoint(float min, float max)
    {
        float midPoint = (min + max) / 2;
        return midPoint;
    }
    public override void UpdateState(LockpickingStateManager LMS)
    {

        float x = LMS.Input.AxisX;
        float z = LMS.Input.AxisZ;
        AngleCalculation(z, x);
        SuccesPoint(LMS, z);
        DurabilityContral(LMS);
        DamageControl(z);
    }
    private void AngleCalculation(float AxisZ, float AxisX)
    {
        if (Mathf.Abs(AxisZ) < _inputThreshold)
        {
            _angle += AxisX * _lockPickingSpeed * Time.deltaTime;
        }
        _lockpickBigger.transform.localRotation = Quaternion.Euler(_StartAngleOfLockpick + _angle, _lockpinkAngle.eulerAngles.y, _lockpinkAngle.eulerAngles.z);
        _angle = Mathf.Clamp(_angle, _minAngle, _maxAngle);

        if (_angle >= _minZRot && _angle <= _maxZRot)
        {
            _successRate = 1;
            return;
        }
        else
        {
            float _min = MinMax(_minZRot, _angle);
            float _max = MinMax(_angle, _maxZRot);

            _successRate =
            _min >= _max
            ? _min 
            : _max;
        }
    }
    private void SuccesPoint(LockpickingStateManager LMS, float AxisZ)
    {
        _completionRate =
               AxisZ >= 0.1f
               ? Mathf.MoveTowards(_completionRate, _successRate, AxisZ * Time.deltaTime)
               : Mathf.MoveTowards(_completionRate, 0, Time.deltaTime);

        _completionRate = Mathf.Clamp(_completionRate, 0, 1);

        if (_completionRate < 1)
        {
            return;
        }
        Debug.Log("Completion");
        _animator.enabled = true;
        
        LMS.SwitchingState(LMS._exitState);
        LMS.enabled = false;
    }
    private void DurabilityContral(LockpickingStateManager LMS)
    {
        if (_durability > 0)
        { 
            return;
        }
        LMS.SwitchingState(LMS._exitState);
        Debug.Log("Durability");
    }
    private void DamageControl(float AxisZ)
    {
        if (Mathf.Abs(_completionRate - _successRate) <= 0.02f && _completionRate != 1 && Mathf.Abs(AxisZ) >= _inputThreshold)
        {
            LockpicksDamage();
            return;
        }
        else
        {
            _lockpickSmaller.transform.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(90, 0, _completionRate));
        }
    }

    private void LockpicksDamage()
    {
        _durability -= _durabilityBreakSpeed * Time.deltaTime;
        _lockpickSmaller.transform.localRotation *= Quaternion.Euler(Random.insideUnitSphere * _shakeIntensity);
    }

    /// <summary>
    /// if you want to find min Angle you set MinMax(min, _angle) , if otherwise MinMax(_angle, max)
    /// </summary>
    private float MinMax(float MinOrAngle,float AngleOrMax)
    {
        if (AngleOrMax <= MinOrAngle && AngleOrMax >= MinOrAngle - _rangeKoeficient)
        {
            float m =   ((AngleOrMax - MinOrAngle) / _rangeKoeficient) + 1; // 1 = rangeKoeficient/rangeKoeficient
            return m;
        }
        return 0;

    }
}
