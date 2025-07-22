using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        other.transform.GetComponent<HealthManagement>().Damage(100f,0);
    }
}
