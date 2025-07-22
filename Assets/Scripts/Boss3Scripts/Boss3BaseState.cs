using UnityEngine;

public abstract class Boss3BaseState
{
    protected Boss3StateManager boss3;
    public abstract void EnterState(Boss3StateManager boss3);
    public abstract void UpdateState(float deltaTime);
    public abstract void FixedUpdateState(float fixedDeltaTime);



    public abstract void LeaveState();
}
