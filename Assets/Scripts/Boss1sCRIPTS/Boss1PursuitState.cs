using UnityEngine;
[System.Serializable]
public class Boss1PursuitState : Boss1BaseState
{
    private Vector3 targetPos;
    public float speed;
    public float stoppingDist = 0.5f;      

    
    public override void EnterState(Boss1StateManager boss1)
    {
        this.boss1 = boss1;

        boss1.boss1Animator.SetBool("run", true);
        CalculateTargetPos();



    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
        
    }

    public override void UpdateState(float deltaTime)
    {
        CalculateTargetPos();
        boss1.transform.LookAt(new Vector3(targetPos.x, boss1.transform.position.y, targetPos.z));
        Vector3 moveDir = (targetPos - boss1.transform.position).normalized;
        
        boss1.transform.GetComponent<CharacterController>().Move(moveDir * speed*deltaTime);

        
        if (Vector3.Distance(boss1.transform.position, targetPos) <= stoppingDist)
        {
            boss1.boss1Animator.SetBool("run", false);
            boss1.ChangeState(boss1.Boss1AttacksCombinationState);
        }
    }

    private void CalculateTargetPos()
    {
        float distance = (boss1.player.position - boss1.transform.position).magnitude;
        float targetspeed = boss1.player.GetComponent<PlayerStateManager>().PlayerMovementState.GetNetVelocity().magnitude;
        Vector3 targetForwardDirection = boss1.player.GetComponent<PlayerStateManager>().PlayerMovementState.GetNetVelocity().normalized;
        targetspeed = Mathf.Max(targetspeed, 0.01f);
        float targetPosmag = distance / (targetspeed * speed);

        targetPos = boss1.player.position + targetForwardDirection * targetPosmag;
    }
}
