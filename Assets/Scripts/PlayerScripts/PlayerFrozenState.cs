using UnityEngine;

[System.Serializable]
public class PlayerFrozenState : PlayerBaseState
{
    public float frozenTime;
    public GameObject iceCube;
    public float damageInThisState = 10f;
    public override void EnterState(PlayerStateManager player)
    {
        player.transform.GetComponent<HealthManagement>().Damage(damageInThisState, 0);
        this.player = player;
        iceCube.SetActive(true);
        player.playerAnimator.Play("Tpose");
        player.timeToGoToFrozenState = false;

        player.StartCoroutine(player.ExecuteAfterSomeTime(frozenTime,()=> {

            player.ChangeState(player.PlayerMovementState);
        
        }));

    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
        
        iceCube.SetActive(false);
        player.playerAnimator.Play("Movement");
    }

    public override void UpdateState(float deltaTime)
    {
        
    }
}
