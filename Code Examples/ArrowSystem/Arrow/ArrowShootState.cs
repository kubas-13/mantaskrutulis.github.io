using UnityEngine;

public class ArrowShootState : ArrowBaseState
{
    private Rigidbody _rb;
    private float _angle;
    private float _velocity;
    public override void SetupState(ArrowStateManager ASM)
    {
       _rb = ASM.GetComponent<Rigidbody>();
       _angle = ASM.Prefab.Angle * Mathf.Deg2Rad;
    }
    public override void StartState(ArrowStateManager ASM)
    {
        _velocity = ASM.Velocity;
        Push(ASM);
        Debug.Log("Start Shoot State");
    }
    private void Push(ArrowStateManager ASM)
    {
        _rb.linearVelocity = (_velocity * Mathf.Cos(_angle) * ASM.transform.forward) + (_velocity * Mathf.Sin(_angle) * ASM.transform.up);
    }
}
