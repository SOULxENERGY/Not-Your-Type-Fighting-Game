using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Boss1AttacksCombinationState : Boss1BaseState
{

    private List<Boss1BaseState> boss1AllAttackStates = new List<Boss1BaseState>();
   // private int currentAttackIndex = 0;
    public WeaponColliderOfBoss1 weapon;
    public AudioClip dialouge;

    public override void EnterState(Boss1StateManager boss1)
    {
        if (boss1.currentAttackIndex == 0)
        {
          
            boss1.mouth.clip = dialouge;
            boss1.mouth.Play();

        }
        this.boss1 = boss1;
        weapon.canCollide = false;
        // boss1AllAttackStates = new List<Boss1BaseState> { boss1.boss1Attack1State, boss1.boss1Attack2State,boss1.boss1Attack1State };

        if (boss1.currentAttackIndex >= boss1.allLightAttacksSequence.Count)
        {
            boss1.currentAttackIndex = 0;
            
            boss1.ChangeState(boss1.boss1IdleState);
        }
        else
        {
            weapon.canCollide = true;
            if (boss1.allLightAttacksSequence[boss1.currentAttackIndex] == boss1.boss1Attack1State)
            {
                weapon.attackType = -1;
            }
            else
            {
                weapon.attackType = +1;
            }
            boss1.ChangeState(boss1.allLightAttacksSequence[boss1.currentAttackIndex]);
            boss1.currentAttackIndex++;
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
        
    }
}

    // Start is called once before the first execution of Update after the MonoBehaviour is created


