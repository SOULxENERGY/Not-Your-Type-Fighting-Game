using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]

public class PlayerBlockState : PlayerBaseState
{
    private int CurrentBlockMoveIndex=0;
    public Transform shieldVfx;
    public override void EnterState(PlayerStateManager player)
    {
        player.transform.LookAt(new Vector3(player.enemy.position.x, player.transform.position.y, player.enemy.position.z));
        this.player = player;
        shieldVfx.gameObject.SetActive(true);
      for(int i = 0; i < player.blockingMovesStats.Count; i++)
        {
            if (player.blockingMovesStats[i])
            {
                CurrentBlockMoveIndex = i;
                break;
            }
        }

        if (CurrentBlockMoveIndex == 0)
        {
            player.playerAnimator.SetBool("LeftBlock", true);
        }else if (CurrentBlockMoveIndex == 1)
        {
            player.playerAnimator.SetBool("FrontBlock", true);
        }else if (CurrentBlockMoveIndex == 2)
        {
            player.playerAnimator.SetBool("RightBlock", true);
        }
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        
    }

    public override void LeaveState()
    {
        
    }

    public override void UpdateState(float deltaTime)
    {
        if (player.blockingMovesStats[CurrentBlockMoveIndex]==false)
        {
            if (CurrentBlockMoveIndex == 0)
            {
                player.playerAnimator.SetBool("LeftBlock", false);
            }
            else if (CurrentBlockMoveIndex == 1)
            {
                player.playerAnimator.SetBool("FrontBlock", false);
            }
            else if (CurrentBlockMoveIndex == 2)
            {
                player.playerAnimator.SetBool("RightBlock", false);
            }
            player.ChangeState(player.PlayerMovementState);

            shieldVfx.gameObject.SetActive(false);

           
            
        }
    }

 
}
