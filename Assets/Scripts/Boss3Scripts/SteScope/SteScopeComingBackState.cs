using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class SteScopeComingBackState : SteScopeBaseState
{
    public float speed = 10f;
    private bool onlyOnce = false;
    private Rigidbody rb;
    public AudioClip dialouge;
  
    public override void EnterState(SteScopeStateManager steScope)
    {
        steScope.GetComponent<AudioSource>().Play();
        if (!steScope.mouth.isPlaying)
        {
            steScope.mouth.clip = dialouge;
            steScope.mouth.Play();
        }
       
        if (!onlyOnce)
        {
            this.steScope = steScope;
            rb = steScope.transform.GetComponent<Rigidbody>();
            
            onlyOnce = true;


        }
        steScope.transform.GetComponent<BoxCollider>().enabled = false;
      
     
        
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
       
        

    }

    public override void LeaveState()
    {
        steScope.transform.GetComponent<BoxCollider>().enabled = true;
    }

    public override void UpdateState(float deltaTime)
    {
        Vector3 targetPos = steScope.relativeBone.position + steScope.distFromRelativeBone;
        Vector3 dir = (targetPos - steScope.transform.position).normalized;
        float remainingDistFromTarget = (targetPos - steScope.transform.position).magnitude;

        if (remainingDistFromTarget > 0.4)
        {
            Vector3 ds = dir * speed * deltaTime;
            if ((steScope.transform.position + ds).magnitude > targetPos.magnitude)
            {
                Debug.Log("direct target");
                ds = targetPos - steScope.transform.position;
            }
            steScope.transform.position += ds;
        }
        else
        {
            steScope.ChangeState(steScope.steScopeIdleState);
        }

    }
}
