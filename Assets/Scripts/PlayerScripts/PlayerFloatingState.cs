using UnityEngine;
[System.Serializable]
public class PlayerFloatingState : PlayerBaseState
{
    public float floatingTime;
    public GameObject signalVfx;
    public float damageInThisState = 10f;
    public override void EnterState(PlayerStateManager player)
    {
        player.timeToGoToFloatingState = false;
        signalVfx.transform.GetComponent<AudioSource>().Play();
        player.transform.GetComponent<HealthManagement>().Damage(damageInThisState, 0);
        this.player = player;
        signalVfx.SetActive(true);
        player.playerAnimator.Play("Hanging Idle");
        player.timeToGoToFrozenState = false;

        player.StartCoroutine(player.ExecuteAfterSomeTime(floatingTime, () => {
            
            player.ChangeState(player.PlayerMovementState);

        }));

    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {

    }

    public override void LeaveState()
    {

        signalVfx.SetActive(false);
        player.playerAnimator.Play("Movement");
    }

    public override void UpdateState(float deltaTime)
    {

    }
}
