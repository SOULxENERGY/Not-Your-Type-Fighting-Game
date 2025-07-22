using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Boss3StateManager : MonoBehaviour
{
    public Boss3BaseState currentState;

    public Transform player;
   [System.NonSerialized] public Vector3 targetPos;
   

    public Boss3IdleState boss3IdleState = new Boss3IdleState();
    public Boss3AttackState boss3AttackState = new Boss3AttackState();


    public Animator boss3Animator;
    public delegate void ExeAfterSomeTime();

    public LevelInitializer levelInitializer;
    public delegate void AttackStateDelegate(Vector3 target);
    public event AttackStateDelegate attackStateEvent;
    public AudioSource mouth;






    void Start()
    {
        ChangeState(boss3IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelInitializer.isLevelStarted)
        {
            currentState.UpdateState(Time.deltaTime);

        }

    }

    private void FixedUpdate()
    {
        if (levelInitializer.isLevelStarted)
        {
            currentState.FixedUpdateState(Time.fixedDeltaTime);
        }

    }

    public void ChangeState(Boss3BaseState state)
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
    public void SetTarget()
    {
        if (attackStateEvent!=null)
        {
            attackStateEvent(targetPos);
        }
        
    }

}
