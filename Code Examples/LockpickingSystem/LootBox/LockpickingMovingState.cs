using UnityEngine;

public class LockpickingMovingState : LockpickingIdleState
{
    private Transform _camera;
    private Transform _cameraLootBox;
    private float _dotProductThreshold;
    private float _distanceThreshold;
    public override void SetupState(LockpickingStateManager LMS)
    {
        _camera = LMS.Camera;
        _dotProductThreshold = LMS.Prefab.DotProductThreshold;
        _distanceThreshold = LMS.Prefab.DistanceThreshold;
        _cameraLootBox = LMS.CameraLootBoxPos;
    }
    public override void StartState(LockpickingStateManager LMS){ }
    public override void UpdateState(LockpickingStateManager LMS)
    {
        _camera.transform.position = Vector3.Lerp(_camera.position, _cameraLootBox.position, Time.deltaTime);
        _camera.transform.localRotation = Quaternion.Lerp(_camera.localRotation, _cameraLootBox.localRotation, Time.deltaTime);
        float dotProduct = Quaternion.Dot(_camera.localRotation, _cameraLootBox.localRotation);
        if (Vector3.Distance(_camera.position, _cameraLootBox.position) <= _distanceThreshold && Mathf.Abs(dotProduct) > _dotProductThreshold)
        {        
            LMS.Lock.SetActive(true);
            LMS.SwitchingState(LMS._lockpicking);
        }
    }
}
