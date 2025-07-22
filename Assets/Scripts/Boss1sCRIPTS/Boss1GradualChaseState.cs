using UnityEngine;

[System.Serializable]
public class Boss1GradualChaseState : Boss1BaseState
{
    public float speed = 7f;
    public float stoppingDist = 12.03f;
    public override void EnterState(Boss1StateManager boss1)
    {
        this.boss1 = boss1;
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
       
    }

    public override void UpdateState(float deltaTime)
    {
        float dist = Vector3.Distance(boss1.transform.position, boss1.player.position);
        Vector3 dir = (boss1.player.position - boss1.transform.position).normalized;
        Vector3 ds = dir * speed * deltaTime;
        if (dist+ds.magnitude > stoppingDist)
        {
            boss1.GetComponent<CharacterController>().Move(ds);
        }
       
    }


}
