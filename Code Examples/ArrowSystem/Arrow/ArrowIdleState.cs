using System.Collections;
using UnityEngine;

public class ArrowIdleState : ArrowBaseState
{
    private float _attackRate;
    public override void SetupState(ArrowStateManager ASM)
    {
        _attackRate = ASM.Prefab.AttackRate;
    }
    public override void StartState(ArrowStateManager ASM)
    {
        ASM.StartCoroutine(GetReadyToShoot(ASM));
        Debug.Log("Start Idle State");

    }
    IEnumerator GetReadyToShoot(ArrowStateManager ASM)
    {
        yield return new WaitForSeconds(_attackRate);
        ASM.SwitchState(ASM.CalculationState);
    }

}
