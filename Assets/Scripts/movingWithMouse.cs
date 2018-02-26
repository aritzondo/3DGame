using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingWithMouse : MonoBehaviour
{
    //public attributes
    public CharacterMovement player;
    public GameObject hub;
    public float sensitivity = 15.0f;   //sensitivity of the rotation with the mouse
    public float rotationSpeed = 20.0f; //the speed of the rotation
    public bool activate = false;
    public float adjustDuration = 1.0f;
    public float rotationDuration = 3.0f;

    //private attributes
    float rotX = 0.0f;  //the ammount of rotation on x
    float rotY = 0.0f;  //the ammount of rotation on y
    Quaternion initialState;
    //variables for the state of the rotator
    bool rotating = false;
    bool returning = false;
    Vector3 nextAngle = new Vector3(0, 0, 0);
    private Quaternion cubeIniRot;
    private Quaternion hubIniRot;
    private float rotTime = 0.0f;
    private float rotDuration = 0.0f;

    void Start()
    {
        initialState = transform.rotation;
        cubeIniRot = transform.rotation;
        rotDuration = adjustDuration;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        //if the mouse has been released
        if (Input.GetMouseButtonUp(0))
        {
            released();
        }
        //if the rotator is activate
        if (activate)
        {
            //calculate the rotation to aply to the cube with the mouse
            rotating = false;
            rotX += Input.GetAxis("Mouse Y") * rotationSpeed * sensitivity * Mathf.Deg2Rad * dt;
            rotY -= Input.GetAxis("Mouse X") * rotationSpeed * sensitivity * Mathf.Deg2Rad * dt;
        }
        //if it is adjusting to an angle or returning to origin
        if (rotating || returning)
        {
            //calculate the next rotation with an interpolation
            rotTime += dt;
            float t = rotTime / rotDuration;
            Quaternion newRot = Quaternion.Slerp(cubeIniRot, Quaternion.Euler(nextAngle), t);//Quaternion.Lerp(transform.rotation, Quaternion.Euler(nextAngle.x, nextAngle.y, nextAngle.z), Time.deltaTime * 2);
            
            //if the next rotation is the same as the actual we ensure the rotation with the destination
            if (Quaternion.Angle(transform.rotation,newRot) == 0.0f) //cambiar por calcular el ángulo enter newRot y transform.rotation
            {
                if (returning)
                {
                    //if it was returning we set the rotation of the hub and end the rotation
                    returning = false;
                    hub.transform.eulerAngles = hub.transform.eulerAngles + nextAngle - cubeIniRot.eulerAngles;
                    rotDuration = adjustDuration;
                }
                if (rotating)
                {
                    //if it was adjusting we start the rotation with the hub
                    rotating = false;
                    returning = true;
                    cubeIniRot = transform.rotation;
                    hubIniRot = hub.transform.rotation;
                    rotTime = 0;
                    nextAngle = new Vector3(0, 0, 0);
                    rotDuration = rotationDuration;
                }
                
            }
            else
            {
                //if it's not the end of the rotation we apply it to the cube and to the hub if it's necessary
                if (returning)
                {
                    Quaternion offset = newRot * Quaternion.Inverse(transform.rotation);
                    hub.transform.rotation *= offset;
                }
                transform.rotation = newRot;
            }
            //update the initial state
            initialState = transform.rotation;
        }
        else
        {
           //apply the rotation of the mouse
            transform.rotation = initialState;
            transform.Rotate(rotX, rotY, 0);
        }
    }

    //calculate the closest angle of rotation (right angles)
    public void adjustAngle()
    {
        float eulerX = transform.rotation.eulerAngles.x;
        float eulerY = transform.rotation.eulerAngles.y;
        float eulerZ = transform.rotation.eulerAngles.z;

        //calculate the next x
        if (eulerX >= 0 && eulerX <= 45)
        {
            nextAngle.x = 0;
        }
        else if (eulerX > 45 && eulerX <= 135)
        {
            nextAngle.x = 90;
        }

        else if(eulerX >135 && eulerX <= 225)
        {
            nextAngle.x = 180;
        }

        else if(eulerX >225 && eulerX <=315)
        {
            nextAngle.x = 270;
        }
        else if (eulerX > 315 || eulerX <=360)
        {
            nextAngle.x = 360;
        }
        //
        //calculate the next y
        if(eulerY >= 0 && eulerY <= 45)
        {
            nextAngle.y = 0;
        }
        else if (eulerY > 45 && eulerY <= 135)
        {
            nextAngle.y = 90;
        }

        else if (eulerY > 135 && eulerY <= 225)
        {
            nextAngle.y = 180;
        }

        else if (eulerY > 225 && eulerY <= 315)
        {
            nextAngle.y = 270;
        }
        else if (eulerY > 315 && eulerY <=360)
        {
            nextAngle.y = 360;
        }
        //
        //calculate the next z
        if (eulerZ >= 0 && eulerZ <= 45)
        {
            nextAngle.z = 0;
        }
        else if (eulerZ > 45 && eulerZ <= 135)
        {
            nextAngle.z = 90;
        }

        else if (eulerZ > 135 && eulerZ <= 225)
        {
            nextAngle.z = 180;
        }

        else if (eulerZ > 225 && eulerZ <= 315)
        {
            nextAngle.z = 270;
        }
        else if (eulerZ > 315 && eulerZ <= 360)
        {
            nextAngle.z = 360;
        }
    }

    //when the cube is clicked the camera and movement of the player are blocked and the cube is activated
    public void clicked() 
    {
        Debug.Log("Cliked");
        if (!(rotating || returning))
        {
            activate = true;
            Camera.main.GetComponent<CameraMovement>().enabled = false;
        }
        else
        {
            Debug.Log("Moving");
        }
        player.GetComponent<CharacterMovement>().SetInteracting(true);

    }

    //when the mouse is released we give back the control to the player and start the adjusting proccess
    void released()
    {
        rotX = 0;
        rotY = 0;
        adjustAngle();
        rotating = true;
        activate = false;
        Camera.main.GetComponent<CameraMovement>().enabled = true;
        player.GetComponent<CharacterMovement>().SetInteracting(false);
        cubeIniRot = transform.rotation;
        rotTime = 0;
    }
}