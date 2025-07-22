using UnityEngine;

[System.Serializable]
public class Boss2FreezingAttackState : Boss2BaseState
{
    public GameObject IceZone;
    public float preAnimationDuration;
    public float duration = 4f;
    public AudioClip dialouge;
    public override void EnterState(Boss2StateManager boss2)
    {
        boss2.mouth.clip = dialouge;
        boss2.mouth.Play();
        this.boss2 = boss2;
        boss2.boss2Animator.Play("Freeze");
        boss2.StartCoroutine(boss2.ExecuteAfterSomeTime(preAnimationDuration,()=> {
            MainFunctionality();

        }));
        

    }

    private void MainFunctionality()
    {
        IceZone.transform.GetComponent<MeshRenderer>().enabled = true;
        IceZone.GetComponent<IceAreaScript>().isItActive = true;

        boss2.StartCoroutine(boss2.ExecuteAfterSomeTime(duration, () =>
        {
            boss2.ChangeState(boss2.boss2ShootingState);
        }));
    }
    public override void FixedUpdateState(float fixedDeltaTime)
    {
       
    }

    public override void LeaveState()
    {
        IceZone.transform.GetComponent<MeshRenderer>().enabled = false;
        IceZone.GetComponent<IceAreaScript>().isItActive = false;
    }

    public override void UpdateState(float deltaTime)
    {
        
    }
}
