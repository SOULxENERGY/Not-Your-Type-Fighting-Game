using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationHandler : MonoBehaviour
{
	 private InputHandlerForCharacterController inputHandlerForCharacter;
	
	[SerializeField] private Transform cameraHolder;
	[SerializeField] private Vector3 cameraPositionDeviation;

	
	

	//Rotation Sensitivity
	public float RotationSensitivity = 6.0f;
	public float minAngle = -45.0f;
	public float maxAngle = 45.0f;

	//Rotation Value
	float xRotate = 0.0f;
	float yRotate = 0.0f;
	public Transform startingPosOfCamera;
	public float upOffset;
	public float belowOffset;
	

    private void Start()
    {
		inputHandlerForCharacter = GameObject.Find("InputHandler").GetComponent<InputHandlerForCharacterController>();
		
    }
    void Update()
	{
		Vector2 rotationalInput = inputHandlerForCharacter.GetRotationalInput();
		xRotate += rotationalInput.y*Time.deltaTime*RotationSensitivity;
		xRotate=Mathf.Clamp(xRotate, minAngle, maxAngle);
		transform.localEulerAngles = new Vector3(-xRotate, 0, 0);

		Vector3 dist = new Vector3(transform.position.x,startingPosOfCamera.position.y,transform.position.z) - startingPosOfCamera.position;
        if (xRotate > 0)
        {
			transform.position = (startingPosOfCamera.position+dist) + Vector3.down * belowOffset* Mathf.Abs(xRotate/maxAngle);
        }else if (xRotate < 0)
        {
			transform.position = (startingPosOfCamera.position + dist) + Vector3.up * upOffset* Mathf.Abs(xRotate / minAngle);
		}
        else
        {
			transform.position = (startingPosOfCamera.position + dist);
		}
        

       


	}

}
