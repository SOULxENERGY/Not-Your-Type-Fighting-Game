using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerBaseState currentState;
    public PhysicsBasedVelocityCalculateor physicsBasedVelocityCalculator;
    public PlayerMovementState PlayerMovementState = new PlayerMovementState();
    public PlayerAttackState playerAttackState = new PlayerAttackState();
   // public PlayerBlockState playerBlockState = new PlayerBlockState();
    public PlayerLeftBlockState playerLeftBlockState = new PlayerLeftBlockState();
    public PlayerRightBlockState playerRightBlockState = new PlayerRightBlockState();
    public PlayerFrozenState playerFrozenState = new PlayerFrozenState();
    public PlayerDashState playerDashState = new PlayerDashState();
    public PlayerFloatingState playerFloatingState = new PlayerFloatingState();
    public PlayerDeathState playerDeathState = new PlayerDeathState();
    public Animator playerAnimator;
    public delegate void ExeAfterSomeTime();
    [System.NonSerialized]public CharacterController cController;
    public event UnityAction attackEvent;
    public delegate void blockEvent(bool left, bool right);
    public event blockEvent blockevent;
    public event UnityAction dashEvent;
   [System.NonSerialized] public Transform enemy;
    public Transform boss1;
    [System.NonSerialized] public bool isLeftBlockActivated = false;
    [System.NonSerialized] public bool isRightBlockActivated = false;


    [System.NonSerialized] public List<bool> blockingMovesStats = new List<bool>(3);
    public InputHandlerForCharacterController inputHandler;
    [System.NonSerialized] public bool timeToGoToFrozenState = false;
    [System.NonSerialized] public bool timeToGoToFloatingState = false;
    public GuideScript guideBox;
    public event UnityAction hitwhenDash;
    void Start()
    {
       
        guideBox.ShowInfo();
        enemy = boss1;
        //index 0 means leftblock ,index 1 means front block , index 2 means right block
        blockingMovesStats.Add(false);
        blockingMovesStats.Add( false);
        blockingMovesStats.Add(false);
      
        
        ChangeState(PlayerMovementState);
        cController = transform.GetComponent<CharacterController>();
        physicsBasedVelocityCalculator = transform.GetComponent<PhysicsBasedVelocityCalculateor>();
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

    public void ChangeState(PlayerBaseState state)
    {
        if (currentState!=null)
        {
            currentState.LeaveState();
        }
        
        currentState = state;
        currentState.EnterState(this);
        
    }

    public IEnumerator ExecuteAfterSomeTime(float t,ExeAfterSomeTime func)
    {
        yield return new WaitForSeconds(t);
        func();

    }

    public void Attack()
    {
        attackEvent.Invoke();
    }

    public void LeftBlockFunction(bool stat)
    {
        //blockingMovesStats[0] = stat;
        isLeftBlockActivated = stat;
        if (stat)
        {
            blockevent(true,false);
        }
        
    }

    public void RightBlockFunction(bool stat)
    {
        //blockingMovesStats[2] = stat;
        isRightBlockActivated = stat;
        if (stat)
        {
            blockevent(false, true);
        }
    }

    public int GetCurrentBlockTytpe()
    {
        if (currentState == playerLeftBlockState)
        {
            return -1;
        }else if (currentState == playerRightBlockState)
        {
            return +1;
        }
        else
        {
            return 0;
        }
    }

    public void DashSkill()
    {
        dashEvent.Invoke();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer==12 || hit.gameObject.layer == 7)
        {
            if (hitwhenDash != null)
            {
                hitwhenDash.Invoke();
            }
          
        }
    }

    

}
