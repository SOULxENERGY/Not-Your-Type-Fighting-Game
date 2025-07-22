using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [System.NonSerialized] public bool canCollide = false;
    public GameObject bloosVfx;
    public Transform tip;
    public AudioClip whoosh;
    public AudioClip Slash;
   [System.NonSerialized] public AudioSource src;
    



    private void Start()
    {
        src = transform.GetComponent<AudioSource>();

    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (canCollide )
        {
            
            src.clip = Slash;
            src.Play();
            
            bloosVfx.transform.position = new Vector3(other.transform.position.x, tip.position.y, other.transform.position.z);
            bloosVfx.transform.LookAt(  Camera.main.transform.position);

            bloosVfx.SetActive(true);
            bloosVfx.GetComponent<ParticleSystem>().Emit(1);
            if (other.GetComponent<HealthManagerOfBoss1>())
            {
                other.GetComponent<HealthManagerOfBoss1>().Damage(3f);
            }

            if (other.GetComponent<BellScript>())
            {
                other.GetComponent<BellScript>().Bell();
            }
          
            canCollide = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
