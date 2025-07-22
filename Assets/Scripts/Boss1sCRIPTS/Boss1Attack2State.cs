using UnityEngine;

[System.Serializable]
public class Boss1Attack2State : Boss1BaseState
{

    public float duration;
    
    public override void EnterState(Boss1StateManager boss1)
    {
        boss1.boss1GradualChaseState.EnterState(boss1);
        boss1.transform.LookAt(new Vector3(boss1.player.transform.position.x, boss1.transform.position.y, boss1.player.transform.position.z));
        this.boss1 = boss1;
       
       
        boss1.boss1Animator.Play("Attack2");
        
        boss1.StartCoroutine(boss1.ExecuteAfterSomeTime(duration, () =>
        {
            
            

            boss1.ChangeState(boss1.Boss1AttacksCombinationState);
            

        }));
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
        
    }

    public override void UpdateState(float deltaTime)
    {
        boss1.boss1GradualChaseState.UpdateState(deltaTime);
    }
}
