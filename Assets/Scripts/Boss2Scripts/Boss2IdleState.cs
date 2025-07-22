using UnityEngine;

[System.Serializable]
public class Boss2IdleState : Boss2BaseState
{
    public float restingTime = 5f;
    public override void EnterState(Boss2StateManager boss2)
    {
        this.boss2 = boss2;
        boss2.boss2Animator.Play("Box Idle");
       boss2.StartCoroutine( boss2.ExecuteAfterSomeTime(restingTime, () =>
        {
            float dist = (boss2.player.position - boss2.transform.position).magnitude;
            if (dist < 30)
            {
                boss2.ChangeState(boss2.boss2FreezingAttackState);
            }
            else
            {
                boss2.ChangeState(boss2.boss2ShootingState);
            }
           
            
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
       
    }
}
