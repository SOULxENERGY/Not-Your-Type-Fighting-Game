using UnityEngine;

public abstract class Boss1BaseState
{
    protected Boss1StateManager boss1;
    public abstract void EnterState(Boss1StateManager boss1);
    public abstract void UpdateState(float deltaTime);
    public abstract void FixedUpdateState(float fixedDeltaTime);

   

    public abstract void LeaveState();
}
