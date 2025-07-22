using UnityEngine;

public class RigidBodySword : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool canMove = true;
    public Transform target;
    private Vector3 dir;
    public float speed = 10f;
    void Start()
    {
        dir = (target.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void FixedUpdate()
    {
        if (canMove)
        {

            

             transform.GetComponent<Rigidbody>().linearVelocity = dir * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        canMove = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided");
        canMove = false;
    }
}
