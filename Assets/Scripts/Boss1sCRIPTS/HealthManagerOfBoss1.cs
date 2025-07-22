using UnityEngine;
using UnityEngine.UI;

public class HealthManagerOfBoss1 : MonoBehaviour
{
    public Image healthBarImage;
    public float maxHealth = 100;
    private float currentHealth;
    public Transform level2;
    public DoorScript Level1door;
    public bool isItLastBoss = false;
    public GameObject winWindow;
    public AudioSource mouth;
    public AudioClip ahh;
    
    
 
    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            if (Level1door)
            {
                Level1door.open = true;
            }

            if (level2)
            {
                level2.gameObject.SetActive(true);
            }
            if (isItLastBoss)
            {
                winWindow.SetActive(true);
            }
            
            Destroy(gameObject);
        }

    }

    public void Heal(float healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }


    public void Damage(float damagePower)
    {

        if (isItLastBoss)
        {
            mouth.clip = ahh;
            mouth.Play();
        }
        currentHealth -= damagePower;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        
    }


    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
