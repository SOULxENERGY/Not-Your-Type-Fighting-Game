using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerMovementState : PlayerBaseState
{
    [SerializeField] private Camera cameraMain; // Reference to your FreeLook camera
    [SerializeField] private InputHandlerForCharacterController inputHandler;

    
    public float speed;
    private CharacterController cController;
    public float rotationSpeed = 20f;
    private Vector3 controlledVelo = Vector3.zero;
    private Vector3 physicsBasedVelo;
    private bool onlyOnceExecuted = false;
    public GameObject blockPopUp;
   
    public override void EnterState(PlayerStateManager player)
    {
      
        player.playerAnimator.Play("Movement");
        this.player = player;
        player.physicsBasedVelocityCalculator.continuousForces = new List<Vector3>() { };
        player.physicsBasedVelocityCalculator.continuousForces.Add(new Vector3(0, -98, 0));

        if (!onlyOnceExecuted)
        {
           
            ExecuteOnlyOnce();
            
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
        if (player.transform.GetComponent<HealthManagement>().GetCurrentHealth() <= 0)
        {
            player.ChangeState(player.playerDeathState);
        }
        if (player.timeToGoToFrozenState)
        {
            player.ChangeState(player.playerFrozenState);
        }else if (player.timeToGoToFloatingState)
        {
            player.ChangeState(player.playerFloatingState);
        }
        Vector3 walkingInput = inputHandler.GetWalkingInput().normalized;
     
        Vector3 rotationalInput = inputHandler.GetRotationalInput();
        player.transform.eulerAngles += new Vector3(0, 1, 0) * rotationSpeed * deltaTime * rotationalInput.x;
        controlledVelo = player.transform.rotation * walkingInput * speed;
        if (player.cController.isGrounded)
        {
            player.physicsBasedVelocityCalculator.physicsBasedVelocity = new Vector3(player.physicsBasedVelocityCalculator.physicsBasedVelocity.x, 0, player.physicsBasedVelocityCalculator.physicsBasedVelocity.z);
        }
       player.cController.Move((controlledVelo+player.physicsBasedVelocityCalculator.physicsBasedVelocity)*deltaTime);
        player.playerAnimator.SetFloat("veloX", walkingInput.x);
        player.playerAnimator.SetFloat("veloZ", walkingInput.z);
       

     

    }

    private void ExecuteOnlyOnce()
    {
        player.attackEvent += () =>
        {
            if (player.currentState == player.PlayerMovementState)
            {
                
               
                
                player.ChangeState(player.playerAttackState);
            }
            
        };

        player.blockevent += (bool l, bool r) =>
        {
            if (player.currentState == player.PlayerMovementState)
            {
                player.playerAnimator.Play("Movement");
                player.playerAnimator.SetBool("LeftBlock", false);
                player.playerAnimator.SetBool("RightBlock", false);

                if (inputHandler.GetWalkingInput().magnitude == 0 & inputHandler.GetRotationalInput().magnitude==0)
                {
                    if (l)
                    {

                        player.ChangeState(player.playerLeftBlockState);
                    }
                    else
                    {
                        player.ChangeState(player.playerRightBlockState);
                    }
                }
                else
                {
                    if (!blockPopUp.activeInHierarchy)
                    {
                        blockPopUp.SetActive(true);
                    }
                }
              
            }
       
        };

        player.dashEvent += () =>
        {
            if (player.currentState == player.PlayerMovementState)
            {
                player.ChangeState(player.playerDashState);
            }
                
        };

        
        onlyOnceExecuted = true;
    }

    public Vector3 GetNetVelocity()
    {
        return physicsBasedVelo + controlledVelo;
    }

    
}
