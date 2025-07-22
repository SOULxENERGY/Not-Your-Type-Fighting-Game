using UnityEngine;

public class IceAreaScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform player;
    [System.NonSerialized] public bool isItActive = false;
    public ParticleSystem iceparticle;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float dist = (player.position - transform.position).magnitude;
        //29
        if (isItActive)
        {
            if (!iceparticle.isPlaying)
            {
                iceparticle.Play();
            }
            
        }else
        {
            if (iceparticle.isPlaying)
            {
                iceparticle.Pause();
                iceparticle.Clear();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isItActive)
        {
            Debug.Log(other.transform.name);
            transform.GetComponent<AudioSource>().Play();
            if (other.transform.name == "Player")
            {
                PlayerStateManager psm = other.transform.GetComponent<PlayerStateManager>();
                if (psm)
                {
                    psm.timeToGoToFrozenState = true;
                }
                isItActive = false;
            }
            
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (isItActive)
        {
           // Debug.Log(other.transform.name);
            transform.GetComponent<AudioSource>().Play();
            if (other.transform.name == "Player")
            {
                PlayerStateManager psm = other.transform.GetComponent<PlayerStateManager>();
                if (psm)
                {
                    psm.timeToGoToFrozenState = true;
                }
                isItActive = false;
            }

        }
    }
}
