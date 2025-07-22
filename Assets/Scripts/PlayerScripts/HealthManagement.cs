using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image healthBarImage;
    public float maxHealth = 100;
   private float currentHealth;
    private PlayerStateManager playerStateManager;
    void Start()
    {
        currentHealth = maxHealth;
        playerStateManager = transform.GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
        
    }

    public void Heal(float healing)
    {
        currentHealth += healing;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }


    public bool Damage(float damagePower,int attackType)
    {
        if (attackType != 0)
        {
            if (attackType == playerStateManager.GetCurrentBlockTytpe())
            {
                return false;
            }
        }
      
        currentHealth -= damagePower;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        return true;
    }
    

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
