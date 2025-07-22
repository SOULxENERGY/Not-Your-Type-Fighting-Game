using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class SteScopeStateManager : MonoBehaviour
{
    public SteScopeBaseState currentState;
    public Boss3StateManager boss3StateManager;

    public Transform player;

    public SteScopeIdleState steScopeIdleState = new SteScopeIdleState();
    public SteScopeGoToTargetState SteScopeGoToTargetState = new SteScopeGoToTargetState();
    public SteScopeComingBackState steScopeComingBackState = new SteScopeComingBackState();
   
    public delegate void ExeAfterSomeTime();
    public delegate void CollisionDelegate(Collision collision);
    public event CollisionDelegate collisionEvent;
    [System.NonSerialized] public Vector3 targetPos;
    public Transform relativeBone;
    [System.NonSerialized] public Vector3 distFromRelativeBone;
    public AudioSource mouth;







    void Start()
    {
        
        ChangeState(steScopeIdleState);
        distFromRelativeBone = transform.position - relativeBone.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState!=null)
        {
            currentState.UpdateState(Time.deltaTime);
        }
            

        

    }

    private void FixedUpdate()
    {

        if (currentState != null)
        {
            currentState.FixedUpdateState(Time.fixedDeltaTime);
        }
           
        

    }

    public void ChangeState(SteScopeBaseState state)
    {
        if (currentState != null)
        {
            currentState.LeaveState();
        }

        currentState = state;

        currentState.EnterState(this);
    }

    public IEnumerator ExecuteAfterSomeTime(float t, ExeAfterSomeTime func)
    {

        yield return new WaitForSeconds(t);
        func();

    }
    private void OnEnable()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionEvent.Invoke(collision);
    }
}
