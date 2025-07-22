using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerAttackState : PlayerBaseState
{
    public List<AttackAnimationDetails> allAttackAnimations;
    private int currentAttackAnimationId = 0;
    public GameObject swordVfx;
    public WeaponCollider swordCollider;
    public AudioSource srcOfSword;
    public AudioClip whoosh;

    public override void EnterState(PlayerStateManager player)
    {
        if (player.enemy)
        {
            player.transform.LookAt(new Vector3(player.enemy.position.x, player.transform.position.y, player.enemy.position.z));
        }
        
      //  swordCollider.canCollide = true;
        player.playerAnimator.Play(allAttackAnimations[currentAttackAnimationId].name);


        player.StartCoroutine(player.ExecuteAfterSomeTime(allAttackAnimations[currentAttackAnimationId].duration-0.12f,()=> {

            swordVfx.transform.GetComponent<ProceduralSlash>().SetPositionRotation();
            swordVfx.SetActive(true);
            swordVfx.transform.GetComponent<ParticleSystem>().Emit(1);

        }));



        player.StartCoroutine(player.ExecuteAfterSomeTime(0.1f, () => { swordCollider.canCollide = true;srcOfSword.clip = whoosh;srcOfSword.Play(); }));
      
        player.StartCoroutine(player.ExecuteAfterSomeTime(allAttackAnimations[currentAttackAnimationId].duration+0.4f, () =>
        {
            //Debug.Log("leaving attack state");
            currentAttackAnimationId++;
            if (currentAttackAnimationId >= allAttackAnimations.Count)
            {
                currentAttackAnimationId = 0;
            }
            // player.playerAnimator.Play("Movement");
            // swordVfx.SetActive(false);
            swordCollider.canCollide = false;
            player.ChangeState(player.PlayerMovementState);
        }));
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

[System.Serializable]
public class AttackAnimationDetails
{
    public string name;
    public float duration;
}