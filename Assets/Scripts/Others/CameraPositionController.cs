using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform objectRepresentingPlayer;
    [SerializeField] private Transform objectRepresentingCamera;
    private Vector3 startingPosOfCamera;
    public float speed = 10;
    private Transform obstacle;
    public float offset;
    private bool needOffset = true;
    public LayerMask layermask;

    

    private void Start()
    {
        startingPosOfCamera = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, (objectRepresentingPlayer.position-transform.position).normalized);
        RaycastHit hit;
        Ray ray2 = new Ray(objectRepresentingCamera.position, objectRepresentingCamera.forward);
        RaycastHit hit2;

       // Debug.DrawRay(transform.position, (objectRepresentingPlayer.position - transform.position).normalized * 10f);
        if (Physics.Raycast(ray, out hit, Vector3.Distance(objectRepresentingPlayer.position,transform.position), layermask))
        {
           // Debug.Log(hit.transform.name);
            if (hit.transform.name != objectRepresentingPlayer.transform.name)
            {
                //transform.localPosition += new Vector3(0, 0, 1) * speed * Time.deltaTime;
              
                if (Vector3.Distance(objectRepresentingPlayer.position, transform.position) > 0f)
                {
                    transform.position +=  Vector3.ProjectOnPlane((objectRepresentingPlayer.transform.position - transform.position).normalized, Vector3.up) * speed * Time.deltaTime;
                 
                    needOffset = true;
                }
                else
                {
                    
                }
               
            }
            else
            {

                
                if (Physics.Raycast(ray2, out hit2, Vector3.Distance(objectRepresentingPlayer.position, objectRepresentingCamera.position), layermask))
                {
                    if(hit2.transform.name == objectRepresentingPlayer.transform.name)
                    {
                        
                        transform.localPosition = Vector3.Lerp(transform.localPosition, objectRepresentingCamera.localPosition, Time.deltaTime);   // objectRepresentingCamera.localPosition;
                    }
                    else
                    {
                      
                        if (needOffset)
                        {
                            
                            transform.position += Vector3.ProjectOnPlane((objectRepresentingPlayer.transform.position - transform.position).normalized, Vector3.up) * offset;
                            needOffset = false;
                        }
                    }



                }
                else
                {
                    
                }
              
              // transform.localPosition = Vector3.Lerp(transform.localPosition, startingPosOfCamera, Time.deltaTime * speed);
            }
        }
        else
        {
           
            //transform.localPosition = Vector3.Lerp(transform.localPosition, objectRepresentingCamera.localPosition, Time.deltaTime);
            if (Physics.Raycast(ray2, out hit2, Vector3.Distance(objectRepresentingPlayer.position, objectRepresentingCamera.position), layermask))
            {
                if (hit2.transform.name == objectRepresentingPlayer.transform.name)
                {
                    
                    transform.localPosition = Vector3.Lerp(transform.localPosition, objectRepresentingCamera.localPosition, Time.deltaTime);   // objectRepresentingCamera.localPosition;
                }
            }
        }
    }
}
