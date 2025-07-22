using UnityEngine;

public abstract class Boss2BaseState 
{
    protected Boss2StateManager boss2;
    public abstract void EnterState(Boss2StateManager boss2);
    public abstract void UpdateState(float deltaTime);
    public abstract void FixedUpdateState(float fixedDeltaTime);



    public abstract void LeaveState();
}
