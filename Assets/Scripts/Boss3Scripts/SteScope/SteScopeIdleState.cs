using UnityEngine;

public class SteScopeIdleState : SteScopeBaseState
{
    private bool onlyOnceTask = false;
    public override void EnterState(SteScopeStateManager steScope)
    {
      
        if (!onlyOnceTask)
        {
            onlyOnceTask = true;

            steScope.boss3StateManager.attackStateEvent += (Vector3 targetPos) =>
            {
                if (steScope.currentState == steScope.steScopeIdleState)
                {
                    steScope.targetPos = targetPos;

                    steScope.ChangeState(steScope.SteScopeGoToTargetState);
                }
            
            };
        }
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
        
    }

    public override void UpdateState(float deltaTime)
    {
        
    }
}
