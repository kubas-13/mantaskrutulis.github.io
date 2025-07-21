using UnityEngine;
public class LockpickingEnter : LockpickingIdleState
{
    private LayerMask _lootBox;
    private Transform _camera;
    private int i;
    private float _range;

    public override void SetupState(LockpickingStateManager LMS)
    {
        _camera = LMS.Camera;
        _range = LMS.Prefab.RangeToLootBox;
        _lootBox = LMS.Prefab.LootBox;
    }
    public override void StartState(LockpickingStateManager LMS){}
    public override void UpdateState(LockpickingStateManager LMS)
    {
        Vector3 direction = _camera.transform.forward;
        if (Physics.Raycast(_camera.transform.position, direction, out RaycastHit hit ,_range, _lootBox))
        {
            if (i == 0)
            {
                LMS.CreateText();
                i = 1;
            }
            if (!LMS.Input.InputE)
            {
                return;
            }
            LMS.DeleteText();
            LMS.MSM.enabled = false;
            LMS.GGSM.enabled = false;
            LMS.Cf.enabled = false;
            LMS.SwitchingState(LMS._movingState);
            LMS.LootBox = hit.transform.parent.gameObject;
            i = 0;
        }
        else
        {
            if (i == 1)
            {
                LMS.DeleteText();
                i = 0;
            }
        }
    }
}
