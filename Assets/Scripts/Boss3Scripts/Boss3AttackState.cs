using UnityEngine;

[System.Serializable]
public class Boss3AttackState : Boss3BaseState
{
    public float delay = 1.4f;
    public SteScopeStateManager steScope;
    
    public override void EnterState(Boss3StateManager boss3)
    {
       
        if (steScope.currentState != steScope.steScopeIdleState)
        {
           // Debug.Log("boss3 cant attack now");
            boss3.ChangeState(boss3.boss3IdleState);
            return;
        }
        boss3.targetPos = boss3.player.position;
        this.boss3 = boss3;
        boss3.boss3Animator.Play("PreAttack");
        boss3.StartCoroutine(boss3.ExecuteAfterSomeTime(delay, () => {

            boss3.SetTarget();
            boss3.ChangeState(boss3.boss3IdleState);
        }));
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
        boss3.boss3Animator.Play("Breathing Idle");
    }

    public override void UpdateState(float deltaTime)
    {
      
    }
}
