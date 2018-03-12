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
    public float time90degrees = 1.0f;

    //private attributes
    float rotX = 0.0f;  //the ammount of rotation on x
    float rotY = 0.0f;  //the ammount of rotation on y
    //variables for the state of the rotator
    bool rotating = false;
    bool returning = false;
    Vector3 nextAngle = new Vector3(0, 0, 0);
    private Quaternion cubeIniRot;
    private float rotTime = 0.0f;   //time rotating
    private float rotDuration = 0.0f;   //duration of the rotation

    void Start()
    {
        cubeIniRot = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            Quaternion newRot = Quaternion.Slerp(cubeIniRot, Quaternion.Euler(nextAngle), t);
            
            //if the next rotation is the same as the actual we ensure the rotation with the destination
            if (Quaternion.Angle(transform.rotation,newRot) <= Mathf.Epsilon) 
            {
                if (returning)
                {
                    //if it was returning we end the rotation and set the time of the next rotation to the adjust time
                    returning = false;
                }
                transform.rotation = Quaternion.Euler(nextAngle);
                if (rotating)
                {
                    //if it was adjusting we start the rotation with the hub
                    rotating = false;
                    returning = true;
                    cubeIniRot = transform.rotation;
                    rotTime = 0;
                    nextAngle = new Vector3(0, 0, 0);
                    float angleBetween = Quaternion.Angle(transform.rotation, Quaternion.Euler(nextAngle));
                    rotDuration = (angleBetween / 90) * time90degrees;
                }
            }
            else
            {
                //if it's not the end of the rotation we apply it to the cube and to the hub if it's necessary
                if (returning)
                {
                    Quaternion offset = newRot * Quaternion.Inverse(transform.rotation);
                    hub.transform.Rotate(offset.eulerAngles, Space.World);
                }
                transform.rotation = newRot;
            }
        }
        else
        {
           //apply the rotation of the mouse
            transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        }
    }

    //calculate the closest angle of rotation (right angles)
    public void adjustAngle()
    {
        float eulerX = transform.rotation.eulerAngles.x;
        float eulerY = transform.rotation.eulerAngles.y;
        float eulerZ = transform.rotation.eulerAngles.z;
        //calculate the closest square angle of the cube
        nextAngle.x = 90 * (Mathf.FloorToInt(eulerX/90) + Mathf.Round((eulerX % 90) / 90));
        nextAngle.y = 90 * (Mathf.FloorToInt(eulerY/90) + Mathf.Round((eulerY % 90) / 90));
        nextAngle.z = 90 * (Mathf.FloorToInt(eulerZ/90) + Mathf.Round((eulerZ % 90) / 90));
        //calcukate the distance to move to calculate the time of the rotation
        float angleBetween = Vector3.Angle(transform.eulerAngles, nextAngle);
        rotDuration = 1;
    }

    //when the cube is clicked the camera and movement of the player are blocked and the cube is activated
    public void clicked() 
    {
        if (!(rotating || returning))
        {
            activate = true;
            Camera.main.GetComponent<CameraMovement>().enabled = false;
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