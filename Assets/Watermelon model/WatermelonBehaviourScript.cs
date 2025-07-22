using UnityEngine;

using System.Collections.Generic;
using System.Collections;

public class WatermelonBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody rb;
    public float delay = 1.1f;
    public Transform player;
    public float forcemag = 10;
    private Vector3 dir;
    private float currentDelayTime = 0f;
    private bool gg = false;
    public GameObject boss1;
    private AudioSource src;
    public AudioSource rollingSrc;
    public AudioClip collisionSfx;
    public AudioClip rollingSfx;
    public AudioClip dialouge;
    private bool canDamage = false;
    public float damage = 10f;
    void Start()
    {
        src = transform.GetComponent<AudioSource>();
        if (src)
        {
            src.clip = dialouge;
            src.Play();
        }
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
       dir = (player.position - transform.position).normalized;
        currentDelayTime = 0f;
        gg = false;
        if (src)
        {
            src.clip = dialouge;
            src.Play();
        }
        
       
       

    }
    private void Update()
    {
        if (currentDelayTime < delay)
        {
            currentDelayTime += Time.deltaTime;
        }
        else
        {
            if (!rollingSrc.isPlaying)
            {
                canDamage = true;
                rollingSrc.Play();
            }
            
        }
    }
    private void FixedUpdate()
    {
        if (currentDelayTime >= delay)
        {

            gg = true;
            rb.linearVelocity = dir * forcemag;
         
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
       
            if (collision.gameObject.tag != "ground")
            {
            
                if (collision.gameObject.tag == "Player")
                {

                src.clip = collisionSfx;
                src.Play();
                HealthManagement healthManager = collision.transform.GetComponent<HealthManagement>();
                if (healthManager != null)
                {
                    if (canDamage)
                    {
                        healthManager.Damage(damage, 0);
                        canDamage = false;
                    }
                    
                }
                }
                else
                {
                if (gg)
                {

                    StartCoroutine(DeactivateAfterSomeTime(1));
                }
                    
                }
            }
        
           
     
    }

    IEnumerator DeactivateAfterSomeTime(float t)
    {
        yield return new WaitForSeconds(t);
        boss1.transform.position = new Vector3(transform.position.x,boss1.transform.position.y,transform.position.z);
        
        boss1.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }
}
