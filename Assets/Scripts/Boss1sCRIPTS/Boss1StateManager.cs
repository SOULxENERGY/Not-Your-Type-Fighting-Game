using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Boss1StateManager : MonoBehaviour
{
    public Boss1BaseState currentState;
    public Boss1IdleState boss1IdleState = new Boss1IdleState();
    public Boss1Attack1State boss1Attack1State = new Boss1Attack1State();
    public Boss1Attack2State boss1Attack2State = new Boss1Attack2State();
    public Boss1HeavyAttackState boss1HeavyAttackState = new Boss1HeavyAttackState();
    public Boss1PreHeavyAttackState boss1PreHeavyAttackState = new Boss1PreHeavyAttackState();
    public Boss1AttacksCombinationState Boss1AttacksCombinationState = new Boss1AttacksCombinationState();
    public Boss1PursuitState boss1PursuitState = new Boss1PursuitState();
    public Boss1GradualChaseState boss1GradualChaseState = new Boss1GradualChaseState();
    public Transform player;
    [System.NonSerialized] public int currentAttackIndex = 0;
    [System.NonSerialized] public List<Boss1BaseState> allLightAttacksSequence = new List<Boss1BaseState>();
   
    public Animator boss1Animator;
    public delegate void ExeAfterSomeTime();
    

    


   [System.NonSerialized] public List<bool> blockingMovesStats = new List<bool>(3);

    public AudioSource mouth;
    void Start()
    {
        allLightAttacksSequence= new List<Boss1BaseState> { boss1Attack1State, boss1Attack2State,boss1Attack1State };
        ChangeState(boss1IdleState);
    }

    // Update is called once per frame
    void Update()
    {
       
        currentState.UpdateState(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(Time.fixedDeltaTime);
    }

    public void ChangeState(Boss1BaseState state)
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
        ChangeState(boss1IdleState);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        boss1PreHeavyAttackState.makeCollidedTrue();
    }



}
