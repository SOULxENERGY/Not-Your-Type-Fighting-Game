using UnityEngine;
using System.Collections.Generic;


public class ProceduralSlash : MonoBehaviour
{
    public Transform handBone;
    private void Start()
    {
      
    }

    public void SetPositionRotation()
    {
        transform.position = handBone.position;
        transform.rotation = handBone.rotation;
    }
}