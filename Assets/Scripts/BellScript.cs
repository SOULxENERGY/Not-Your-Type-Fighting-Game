using UnityEngine;

public class BellScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource src;
    public float maxHealth = 100;
    private float currentHealth;
    public float healthDeductionPerHit = 25F;
    private bool isDestroyed = false;
    private bool isBossConnected = false;
    public Boss3StateManager boss3;
    void Start()
    {
        currentHealth = maxHealth;
        src = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
   

  
    public void Ring()
    {
        if (!isDestroyed)
        {
            src.Play();
        }
    }
    public void Bell()
    {
        if (!isDestroyed)
        {
            src.Play();
            currentHealth -= healthDeductionPerHit;
            if (isBossConnected)
            {
                
                if (boss3)
                {
                   
                    boss3.GetComponent<HealthManagerOfBoss1>().Damage(17f);
                }
                
            }
            if (currentHealth <= 0)
            {
                isDestroyed = true;
                Destroy(this.gameObject,1f);
            }
        }
       
    }

  

   


    public void ActivateConnectionWithBoss()
    {
       
        isBossConnected = true;
    }

    public void DeActivateConnectionWithBoss()
    {
        
        isBossConnected = false;
    }
}
