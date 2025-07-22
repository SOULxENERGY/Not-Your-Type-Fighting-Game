using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Boss2StateManager : MonoBehaviour
{
    public Boss2BaseState currentState;

    public Transform player;
    public Boss2IdleState boss2IdleState = new Boss2IdleState();
    public Boss2ShootingState boss2ShootingState = new Boss2ShootingState();
    public Boss2FreezingAttackState boss2FreezingAttackState = new Boss2FreezingAttackState();
   

    public Animator boss2Animator;
    public delegate void ExeAfterSomeTime();
   
    public LevelInitializer levelInitializer;
    public AudioSource mouth;





  
    void Start()
    {
        ChangeState(boss2IdleState);
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

    public void ChangeState(Boss2BaseState state)
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

  
}
