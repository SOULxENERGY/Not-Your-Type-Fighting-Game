using UnityEngine;

public class WeaponColliderOfBoss1 : MonoBehaviour
{
    [System.NonSerialized] public bool canCollide = false;
    public GameObject bloosVfx;
    public Transform tip;
    public AudioClip whoosh;
    public AudioClip Slash;
    public AudioClip block;
    [System.NonSerialized] public AudioSource src;
    [System.NonSerialized] public int attackType = 0;//-1 means attack playerfrom leftof the player and +1 means opposite of it
    public float damage=8f;



    private void Start()
    {
        src = transform.GetComponent<AudioSource>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (canCollide)
        {
            if (other.GetComponent<HealthManagement>())
            {
               bool d= other.GetComponent<HealthManagement>().Damage(damage, attackType);
                if (d == true)
                {
                    src.clip = Slash;
                    src.Play();

                    bloosVfx.transform.position = new Vector3(other.transform.position.x, tip.position.y, other.transform.position.z);
                    bloosVfx.transform.LookAt(Camera.main.transform.position);

                    bloosVfx.SetActive(true);
                    bloosVfx.GetComponent<ParticleSystem>().Emit(1);


                    
                }
                else
                {
                    src.clip = block;
                    src.Play();
                }
                canCollide = false;
            }

          
         
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
