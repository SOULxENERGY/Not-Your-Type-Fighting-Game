using UnityEngine;

[System.Serializable]
public class Boss3IdleState : Boss3BaseState
{
    public float restingTime = 2f;
    public override void EnterState(Boss3StateManager boss3)
    {
        
        this.boss3 = boss3;
        boss3.StartCoroutine(boss3.ExecuteAfterSomeTime(restingTime, () => {

            boss3.ChangeState(boss3.boss3AttackState);
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
        
        Vector3 lookAtPoint = new Vector3(boss3.player.transform.position.x, boss3.transform.position.y, boss3.player.transform.position.z);
        boss3.transform.LookAt(lookAtPoint);
    }
}
