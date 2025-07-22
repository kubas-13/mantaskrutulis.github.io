
using UnityEngine;

public abstract class MovementBaseState
{
    public abstract void StartState(MovementStateManager MSM);
    public abstract void SetupState(MovementStateManager MSM);
    public abstract void UpdateState(MovementStateManager MSM);
    public abstract void FixedUpdateState(MovementStateManager MSM);
    public abstract void OnCollision(Collision collision,MovementStateManager MSM);
    

}
