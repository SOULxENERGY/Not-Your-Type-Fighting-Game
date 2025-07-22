using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandlerForCharacterController : MonoBehaviour
{
    [SerializeField] private bool enableTouchScreen = false;
    [SerializeField] private FixedJoystick joyStick;
    [SerializeField] private float noiseMag;
   // [SerializeField] private float noiseMag2;

    private Touch currentTouchOnTouchPad = new Touch { };
    private bool iscurrentTouchSetted = false;
    private List<Touch> touchConsideredForDragging = new List<Touch> { };
    private Vector2 prevRotMouse = Vector2.zero;
    public List<int> allUiLayersThatShouldBeIgnoredWhenDetectingIfTouchIsOverAnyUiElement = new List<int> { };
   



    private void Update()
    {
        
        foreach (Touch touch in Input.touches)
        {
            bool isOverUiElement = false;
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = touch.position
            };
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            if (results.Count > 0)
            {
                // Get the first UI element that was hit
                GameObject hitObject = results[0].gameObject;
                // Debug.Log("Touched UI Element: " + hitObject.name);
                if (allUiLayersThatShouldBeIgnoredWhenDetectingIfTouchIsOverAnyUiElement.Count > 0)
                {
                    foreach (int no in allUiLayersThatShouldBeIgnoredWhenDetectingIfTouchIsOverAnyUiElement)
                    {

                        if (hitObject.layer != no)
                        {
                            isOverUiElement = true;
                        }
                    }
                }
                else
                {
                    
                    isOverUiElement = true;
                }
               
                
            }
           


            if (touch.phase == TouchPhase.Began & !isOverUiElement)//!EventSystem.current.IsPointerOverGameObject(touch.fingerId)
            {
                touchConsideredForDragging.Add(touch);
                
                // Do something with the initial touch position
            }

           


        }

        for (int i= 0;i<touchConsideredForDragging.Count;i++)
        {
            Touch touch1 = touchConsideredForDragging[i];
            bool isTouchDestroyed = true;
            foreach (Touch touch in Input.touches)
            {
                if (touch1.fingerId == touch.fingerId)
                {
                    touchConsideredForDragging[i] = touch;
                    isTouchDestroyed = false;
                }
            }
            if (isTouchDestroyed)
            {
                
                touchConsideredForDragging.Remove(touch1);
            }
            
        }
     
        
        
        
    }

    private Vector2 GetTouchDeltaVec()
    {
      //  Debug.Log(Input.GetTouch(0).phase);

     for(int i = 0; i < touchConsideredForDragging.Count; i++)
        {
            Touch tch = touchConsideredForDragging[i];
          
        

            if ( tch.phase==TouchPhase.Moved)  //!EventSystem.current.IsPointerOverGameObject(tch.fingerId) & tch.phase==TouchPhase.Moved
            {
               // Debug.Log("dragging");
                return new Vector2(tch.deltaPosition.x, tch.deltaPosition.y);
            }
        }

        return Vector2.zero;




       /** if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
             if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
             {

                 return new Vector2(0, 0);
             }
          

                
                return new Vector2(Input.GetTouch(Input.touchCount-1).deltaPosition.x, Input.GetTouch(Input.touchCount - 1).deltaPosition.y);
               // return new Vector2(Input.GetTouch(0).deltaPosition.x, Input.GetTouch(0).deltaPosition.y);
            

            
        }

        return new Vector2(0, 0);**/
    }



    public Vector3 GetWalkingInput()
    {
        float horizontalInput = 0;
        float verticalInput = 0f;

        horizontalInput = joyStick.Horizontal;
        verticalInput = joyStick.Vertical;
        


        if (!enableTouchScreen)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
           
        }

        return new Vector3(horizontalInput, 0, verticalInput);
    }

    public Vector2 GetRotationalInput()
    {
        float mouseX = 0;
        float mouseY = 0;
        mouseX = GetTouchDeltaVec().x;
        mouseY = GetTouchDeltaVec().y;
        if (!enableTouchScreen)
        {
           
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            if (new Vector2(mouseX, mouseY).magnitude < noiseMag)
            {
                mouseX = 0;
                mouseY = 0;
            }
        }
       
        // Debug.Log(mouseX+" "+ mouseY);


      

       
        
       /** if(Vector2.Distance(new Vector2(mouseX, mouseY), prevRotMouse) > noiseMag2)
        {
            Debug.Log("too muh distance");
            mouseY = 0;
            mouseX = 0;
        }

        
        prevRotMouse = new Vector2(mouseX, mouseY);**/
       // Debug.Log("x: "+mouseX+" y: "+ mouseY);
        return new Vector2(mouseX, mouseY);
    }
}
