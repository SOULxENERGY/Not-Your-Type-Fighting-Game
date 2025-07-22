using UnityEngine;

[System.Serializable]
public class PlayerDeathState : PlayerBaseState
{

    public GameObject lostWindow;
    
    public override void EnterState(PlayerStateManager player)
    {
        lostWindow.SetActive(true);
        player.playerAnimator.Play("Death");
        

   
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
