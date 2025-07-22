using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdPersonController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera cameraMain; // Reference to your FreeLook camera
    [SerializeField] private InputHandlerForCharacterController inputHandler;
    
  [HideInInspector]  public bool shouldPlayerBeRotatedTowardsCameraForward = true;
    public float speed;
    private CharacterController cController;
    public float rotationSpeed = 20f;
    private Vector3 controlledVelo=Vector3.zero;
    private Vector3 physicsBasedVelo;
    private bool canRotatePlayer = true;
    private bool canMovePlayer = true;
    
    
    
    void Start()
    {
        cController = transform.GetComponent<CharacterController>();
    }

    private void Update()
    {

        
        Vector3 walkingInput = inputHandler.GetWalkingInput().normalized;
        Vector3 rotationalInput = inputHandler.GetRotationalInput();

        if (canRotatePlayer)
        {
            transform.eulerAngles += new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime * rotationalInput.x;
        }
       

        
        controlledVelo = transform.rotation * walkingInput * speed;

        if (canMovePlayer)
        {
            cController.Move(controlledVelo * Time.deltaTime+Vector3.down*9.8f*Time.deltaTime);
        }

        
       

        
        
    }

    

 

    public Vector3 GetVelocity()
    {
        return inputHandler.GetWalkingInput().normalized;
    }

    public void SetPlayerRotationBool( bool stat)
    {
        canRotatePlayer = stat;
    }

    public void SetPlayerMovingBool(bool stat)
    {
        canMovePlayer = stat;
    }


}
