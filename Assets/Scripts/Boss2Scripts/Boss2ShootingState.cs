using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boss2ShootingState : Boss2BaseState
{
    public Transform bulletStartingPos;
    public GameObject bulletPrefab;
    public float timeBetweenTwoBullets = 5f;
    private float timeSincePrevBulletShooted=0f;
    public int numberOfBulletToShoot = 5;
    private int noOfBulletsShootedSinceThisStateStarted = 0;
    public AudioClip dialouge;
    public override void EnterState(Boss2StateManager boss2)
    {
        boss2.mouth.clip = dialouge;
        boss2.mouth.Play();
        this.boss2 = boss2;
        timeSincePrevBulletShooted = timeBetweenTwoBullets;
        noOfBulletsShootedSinceThisStateStarted = 0;
    }

    public override void FixedUpdateState(float fixedDeltaTime)
    {
       
    }

    public override void LeaveState()
    {
       
    }

    public override void UpdateState(float deltaTime)
    {
        boss2.transform.LookAt(new Vector3(boss2.player.position.x, boss2.transform.position.y, boss2.player.position.z));
       // Debug.Log("boss2 shooting state");
        if (timeSincePrevBulletShooted < timeBetweenTwoBullets)
        {
            timeSincePrevBulletShooted += deltaTime;
        }
        else
        {
            if (noOfBulletsShootedSinceThisStateStarted >= numberOfBulletToShoot)
            {
                boss2.ChangeState(boss2.boss2IdleState);
                return;
            }

            if (boss2.player)
            {
                Shoot();
                noOfBulletsShootedSinceThisStateStarted++;
                timeSincePrevBulletShooted = 0f;
            }
           
        }
    }

    private void Shoot()
    {
        
        GameObject newBullet = GameObject.Instantiate(bulletPrefab);
        newBullet.transform.position = bulletStartingPos.position;
        Vector3 targetPos = boss2.player.position + boss2.player.GetComponent<PlayerStateManager>().PlayerMovementState.GetNetVelocity() * 1.81f;
        newBullet.GetComponent<IceBallScript>().target = targetPos;
        newBullet.SetActive(true);



    }
}
