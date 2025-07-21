
using UnityEngine;

public class LockpickingExitState : LockpickingIdleState
{
    private float _savedOriginalRotation;
    public override void SetupState(LockpickingStateManager LSM)
    {
        _savedOriginalRotation = LSM.LockpickNumber1.localRotation.eulerAngles.x;
    }

    public override void StartState(LockpickingStateManager LSM)
    {
        LSM.MSM.enabled = true;
        LSM.GGSM.enabled = true;
        LSM.Cf.enabled = true;
        LSM.SwitchingState(LSM._interactState);
        LSM.Lock.SetActive(false);
        LSM.LockpickNumber1.localRotation = Quaternion.Euler(_savedOriginalRotation, LSM.LockpickNumber1.localRotation.eulerAngles.y, LSM.LockpickNumber1.localRotation.eulerAngles.z);

    }
    public override void UpdateState(LockpickingStateManager LSM)
    {
    }
}
