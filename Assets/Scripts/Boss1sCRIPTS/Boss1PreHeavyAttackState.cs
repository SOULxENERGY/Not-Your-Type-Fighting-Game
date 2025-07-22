using UnityEngine;

[System.Serializable]
public class Boss1PreHeavyAttackState : Boss1BaseState
{

    public float distance = 30;
 
    public float speed = 15;
    private bool collided = false;
    
    public override void EnterState(Boss1StateManager boss1)
    {
       
        this.boss1 = boss1;
        collided = false;
        boss1.boss1Animator.SetBool("run", true);

    
        
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
        boss1.boss1Animator.SetBool("run", false);
    }

    public override void UpdateState(float deltaTime)
    {
        float curentDist = (boss1.player.position - boss1.transform.position).magnitude;
        Vector3 dir = (boss1.transform.position - boss1.player.position).normalized;
        if (curentDist < distance)
        {
            boss1.transform.GetComponent<CharacterController>().Move(dir * speed*deltaTime);
            if (collided == true)
            {
                boss1.ChangeState(boss1.boss1HeavyAttackState);
            }
        }
        else
        {
            boss1.ChangeState(boss1.boss1HeavyAttackState);
        }

        boss1.transform.LookAt(boss1.transform.position+dir*5);

    }

    public void makeCollidedTrue()
    {
        collided = true;
    }
}
