using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState 
{
    protected PlayerStateManager player;
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(float deltaTime);
    public abstract void FixedUpdateState(float fixedDeltaTime);

    public abstract void LeaveState();



}
