using UnityEngine;
[System.Serializable]
public class Boss1HeavyAttackState : Boss1BaseState
{
    public GameObject watermelon;
    public override void EnterState(Boss1StateManager boss1)
    {
        watermelon.transform.position = new Vector3(boss1.transform.position.x, watermelon.transform.position.y, boss1.transform.position.z);
        watermelon.SetActive(true);
        boss1.gameObject.SetActive(false);
      

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
