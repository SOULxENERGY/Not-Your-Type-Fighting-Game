using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class SteScopeGoToTargetState : SteScopeBaseState
{
    private bool onlyOnce = false;
    private Rigidbody rb;
    public float speed = 10f;
    public float suckingDuration=5f;
    private bool isCollided=false;
    private Vector3 collisionPoint;
    private Vector3 target;
    private Vector3 dir;
    public Vector3 targetOffset;
    private BellScript currentConnectedBell;
    public AudioClip dialougeOfLastHeartBeat;
    public AudioClip kanFatDialouge;
    public override void EnterState(SteScopeStateManager steScope)
    {
        isCollided = false;
        currentConnectedBell = null;
        if (!onlyOnce)
        {
            this.steScope = steScope;
            onlyOnce = true;
            rb = steScope.transform.GetComponent<Rigidbody>();
            steScope.collisionEvent += (Collision collision) => {

                if (steScope.currentState == steScope.SteScopeGoToTargetState)
                {
                    if (!isCollided)
                    {
                        isCollided = true;
                        collisionPoint = steScope.transform.position;


                        if (collision.transform.GetComponent<PlayerStateManager>())
                        {
                            steScope.mouth.clip = dialougeOfLastHeartBeat;
                            steScope.mouth.Play();
                            collision.transform.GetComponent<PlayerStateManager>().timeToGoToFloatingState = true;
                        }

                        if (collision.transform.GetComponent<BellScript>())
                        {
                            steScope.mouth.clip = kanFatDialouge;
                            steScope.mouth.Play();
                            currentConnectedBell = collision.transform.GetComponent<BellScript>();
                            currentConnectedBell.Ring();
                            currentConnectedBell.ActivateConnectionWithBoss();
                           

                        }
                        steScope.StartCoroutine(steScope.ExecuteAfterSomeTime(suckingDuration, () => {
                            if (currentConnectedBell)
                            {
                                currentConnectedBell.DeActivateConnectionWithBoss();
                            }
                            steScope.ChangeState(steScope.steScopeComingBackState);



                        }));
                    }
                    
                    
                }
                

            };
        }

        

        target = new Vector3(steScope.targetPos.x, steScope.targetPos.y+8f, steScope.targetPos.z);
    
       // target += targetOffset;
        dir = (target - steScope.transform.position).normalized;
        steScope.GetComponent<AudioSource>().Play();

    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
        if (!isCollided)
        {
            
            rb.linearVelocity = dir * speed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            steScope.transform.position = collisionPoint;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(dir);
        }
        
    }

    public override void LeaveState()
    {
       
    }

    public override void UpdateState(float deltaTime)
    {
        
    }
}
