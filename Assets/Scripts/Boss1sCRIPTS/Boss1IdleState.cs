using UnityEngine;

[System.Serializable]
public class Boss1IdleState : Boss1BaseState
{
    public float restingTime=2.8f;
    public float prevRandomVal = 1;

    public override void EnterState(Boss1StateManager boss1)
    {
        this.boss1 = boss1;
        int randomvalue = Random.Range(1, 11);
      boss1.StartCoroutine(  boss1.ExecuteAfterSomeTime(restingTime,() =>
        {
            if(randomvalue>5 & prevRandomVal > 5)
            {
                randomvalue = 1;
            }
            if (randomvalue <= 5)
            {
                boss1.ChangeState(boss1.boss1PursuitState);
            }
            else
            {
                boss1.ChangeState(boss1.boss1PreHeavyAttackState);
            }

            prevRandomVal = randomvalue;
            
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
       
      
        if (Input.GetKeyDown(KeyCode.A))
        {
            boss1.ChangeState(boss1.boss1PursuitState);
        }
    }
}
