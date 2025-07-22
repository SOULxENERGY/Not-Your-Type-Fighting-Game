using UnityEngine;
[System.Serializable]
public class PlayerDashState : PlayerBaseState
{
    public Transform raycastStartingPos;
    private Vector3 destination;
    public float distanceToCover = 15f;
    public LayerMask enviromentLayers;
    public float stopBeforeObstacle = 0.3f;
    private float speed;
    public float dashTime;
    private Vector3 dashDir;
    public Transform dashVisualizer;
    //for exiting the state
    private Vector3 prevpos;
    private bool onlyOnce = false;
    public override void EnterState(PlayerStateManager player)
    {
        if (!onlyOnce)
        {
            onlyOnce = true;
            player.hitwhenDash += () =>
            {
                if (player.currentState == player.playerDashState)
                {
                  
                    player.ChangeState(player.PlayerMovementState);
                }
                
            };
        }
        this.player = player;
        prevpos = player.transform.position+new Vector3(0.5f,0,0);
        //player.cController.enabled = false;
        dashDir = player.transform.rotation* player.inputHandler.GetWalkingInput().normalized;
       
      
        if (dashDir == Vector3.zero)
        {
            dashDir = player.transform.forward;
        }
        

        if (Physics.Raycast(raycastStartingPos.position, dashDir, out RaycastHit hit, distanceToCover, enviromentLayers))
        {
         
            float dist = hit.distance - stopBeforeObstacle;
            destination = player.transform.position + dashDir * dist;
        }
        else
        {
           
            destination = player.transform.position + dashDir * distanceToCover;
        }

        dashVisualizer.position = destination;
        speed = distanceToCover / dashTime;
        player.playerAnimator.SetFloat("veloX", dashDir.x);
        player.playerAnimator.SetFloat("veloZ", dashDir.z);

    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
       
    }

    public override void LeaveState()
    {
       
        //player.cController.enabled = true;
    }

    public override void UpdateState(float deltaTime)
    {

        float remainingDist = (destination - player.transform.position).magnitude;
    
        dashDir = (destination - player.transform.position).normalized;
        if (remainingDist > 5f)
        {
            Vector3 ds = dashDir * speed * deltaTime;
           /** if ((player.transform.position + ds).magnitude > remainingDist)
            {
                ds = destination - player.transform.position;
            }*/
          
            player.cController.Move(ds);
            //player.transform.position += dashDir * speed * deltaTime;
         

        }
        else
        {
            player.ChangeState(player.PlayerMovementState);
        }
    }
}
