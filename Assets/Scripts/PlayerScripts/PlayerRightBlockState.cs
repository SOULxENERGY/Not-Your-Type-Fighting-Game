using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerRightBlockState : PlayerBaseState
{
    public Transform shieldVfx;

    public override void EnterState(PlayerStateManager player)
    {
        
        if (player.enemy)
        {
            player.transform.LookAt(new Vector3(player.enemy.position.x, player.transform.position.y, player.enemy.position.z));
        }
        this.player = player;
        shieldVfx.gameObject.SetActive(true);
        
        player.playerAnimator.Play("RightBlocking");
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {

    }

    public override void LeaveState()
    {
        player.playerAnimator.Play("Movement");
        player.playerAnimator.SetBool("LeftBlock", false);
        player.playerAnimator.SetBool("RightBlock", false);
        shieldVfx.gameObject.SetActive(false);
    }

    public override void UpdateState(float deltaTime)
    {
        if (!player.isRightBlockActivated)
        {
           
            player.ChangeState(player.PlayerMovementState);

           
        }

        if (player.inputHandler.GetWalkingInput().magnitude > 0 || player.inputHandler.GetRotationalInput().magnitude > 30)
        {
            player.ChangeState(player.PlayerMovementState);
        }
    }
}
