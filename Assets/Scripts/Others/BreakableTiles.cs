using UnityEngine;

public class BreakableTiles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float breakingDelay = 1f;
    private bool isPlayerOverIt = false;
    private float timeSincePlayerOverIt=0f;
    public ParticleSystem smoke;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
          
            isPlayerOverIt = true;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.name == "Player")
        {
      
            isPlayerOverIt = false;
            timeSincePlayerOverIt = 0f;
        }
        
    }

    private void Update()
    {
        
        if (isPlayerOverIt)
        {
            
            if (timeSincePlayerOverIt < breakingDelay)
            {
                timeSincePlayerOverIt += Time.deltaTime;
            }
            else
            {
                smoke.transform.position = transform.position;
                
                smoke.Play();
                smoke.transform.GetComponent<AudioSource>().Play();                
                Destroy(gameObject);
            }

           
        }
    }
}
