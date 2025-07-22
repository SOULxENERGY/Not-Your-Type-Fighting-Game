using UnityEngine;

public class IceBallScript : MonoBehaviour
{
    public ParticleSystem iceHitVfx;
    public float speed = 10f;
    [System.NonSerialized] public Vector3 target;
    public Rigidbody rb;
    private Vector3 dir;
    public float timeToReachPlayer = 3f;
    private float dist;
    public float damage = 5f;

    private void Start()
    {
        dist = ((target + new Vector3(0, 10f, 0)) - transform.position).magnitude;
        dir = ((target + new Vector3(0, 10f, 0)) - transform.position).normalized;
  
    }
    private void OnCollisionEnter(Collision other)
    {
       // Debug.Log(other.transform.name);
        iceHitVfx.transform.position = transform.position;
        iceHitVfx.Play();
        iceHitVfx.transform.GetComponent<AudioSource>().Play();
        if (other.transform.name == "Player")
        {
            HealthManagement healthManagerOfPlayer = other.transform.GetComponent<HealthManagement>();
            if (healthManagerOfPlayer)
            {
                healthManagerOfPlayer.Damage(damage, 0);
            }
        }
    
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
       
        
            rb.linearVelocity = dir * speed;

        
    }
}
