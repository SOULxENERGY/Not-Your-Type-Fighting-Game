using UnityEngine;

public abstract class SteScopeBaseState 
{
    protected SteScopeStateManager steScope;
    public abstract void EnterState(SteScopeStateManager steScope);
    public abstract void UpdateState(float deltaTime);
    public abstract void FixedUpdateState(float fixedDeltaTime);



    public abstract void LeaveState();
}
