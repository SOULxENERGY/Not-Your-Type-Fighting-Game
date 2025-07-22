using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBasedVelocityCalculateor : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerialized] public List<Vector3> continuousForces = new List<Vector3>();
    [System.NonSerialized] public List<Vector3> impulseForces = new List<Vector3>();
    [Range(1f, 50)]
    public float mass;

    [System.NonSerialized] public Vector3 physicsBasedVelocity;


    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        physicsBasedVelocity += CalculateVelocity(Time.fixedDeltaTime);
    }

    private Vector3 CalculateForce(float fixedDeltaTime)
    {
        Vector3 allCurrentContinuousForce = Vector3.zero;
        Vector3 allCurrentImpulseForce = Vector3.zero;
        foreach (Vector3 frc in continuousForces)
        {
            allCurrentContinuousForce += frc;

        }
       

        foreach (Vector3 frc in impulseForces)
        {
            allCurrentImpulseForce += frc;
        }

        impulseForces = new List<Vector3>();
        // Debug.Log(allCurrentContinuousForce);
        return allCurrentContinuousForce + allCurrentImpulseForce;
    }

    private Vector3 CalculateAcc(float mass, float fixedDeltaTime)
    {
        Vector3 currentFrc = CalculateForce(fixedDeltaTime);

        return currentFrc / mass;
    }

    private Vector3 CalculateVelocity(float fixedDeltaTime)
    {
        Vector3 currentVelo = CalculateAcc(mass, fixedDeltaTime) * fixedDeltaTime;
        return currentVelo;

    }


}

