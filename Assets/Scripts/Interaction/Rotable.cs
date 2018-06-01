using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotable : MonoBehaviour {

	//public attributes

    public float sensitivity = 100.0f;   //sensitivity of the rotation with the mouse
    public bool only90degrees = false;
    //private attributes
    protected float rotX = 0.0f;  //the ammount of rotation on x
    protected float rotY = 0.0f;  //the ammount of rotation on y
    //variables for the state of the rotator
    protected Vector3 nextAngle = new Vector3(0, 0, 0);
    protected Quaternion cubeIniRot;
    protected float rotTime = 0.0f;   //time rotating
    protected float rotDuration = 0.0f;   //duration of the rotation
    protected State state = State.Idle;

    protected enum State
    {
        Clicked,
        Idle,
        Adjusting,
        Waiting,
        Rotating
    }
    
    private CharacterMovement player;

    void Start()
    {
        cubeIniRot = transform.rotation;
        player = AudioManager.GetInstance().player.GetComponent<CharacterMovement>();
    }

    virtual protected void FixedUpdate()
    {
        float dt = Time.deltaTime;
        float t;
        Quaternion newRot;
        //if the mouse has been released
        switch (state)
        {
            case (State.Idle):
                //nothing
                break;
           case (State.Adjusting): //going to a "square" angle
                rotTime += dt;
                t = rotTime / rotDuration;
                newRot = Quaternion.Slerp(cubeIniRot, Quaternion.Euler(nextAngle), t);
                if (Quaternion.Angle(transform.rotation, newRot) <= Mathf.Epsilon)
                {
                    transform.rotation = Quaternion.Euler(nextAngle);
                    state = State.Idle;
                    cubeIniRot = transform.rotation;
                }
                else
                {
                    transform.rotation = newRot;
                }
                break;
           case (State.Clicked):   //rotated by the player
                //if the mouse has been released
                if (!Input.GetMouseButton(1))
                {
                    released();
                    break;
                }
                rotX += Input.GetAxis("Mouse Y") * sensitivity * dt;
                rotY -= Input.GetAxis("Mouse X") * sensitivity * dt;
                //apply the rotation of the mouse
                transform.localRotation = Quaternion.Euler(rotX, rotY, 0) * cubeIniRot;
                break;
        }
    }

    //when the cube is clicked the camera and movement of the player are blocked and the cube is activated
    public virtual void clicked()
    {
        if (state == State.Idle || state == State.Adjusting)
        {
            Camera.main.GetComponent<CameraMovement>().enabled = false;
            player.GetComponent<CharacterMovement>().SetInteracting(true);
            state = State.Clicked;
        }
    }

    //when the mouse is released we give back the control to the player and start the adjusting proccess
    public virtual void released()
    {
        rotX = 0;
        rotY = 0;
        if (only90degrees)
        {
            adjustAngle();
            state = State.Adjusting;
            rotTime = 0;
        }
        else
        {
            state = State.Idle;
        }
        cubeIniRot = transform.rotation;

        Camera.main.GetComponent<CameraMovement>().enabled = true;
        player.GetComponent<CharacterMovement>().SetInteracting(false);

    }

    //calculate the closest angle of rotation (right angles)
    protected void adjustAngle()
    {
        float eulerX = transform.rotation.eulerAngles.x;
        float eulerY = transform.rotation.eulerAngles.y;
        float eulerZ = transform.rotation.eulerAngles.z;
        //calculate the closest square angle of the cube
        nextAngle.x = 90 * (Mathf.FloorToInt(eulerX / 90) + Mathf.Round((eulerX % 90) / 90));
        nextAngle.y = 90 * (Mathf.FloorToInt(eulerY / 90) + Mathf.Round((eulerY % 90) / 90));
        nextAngle.z = 90 * (Mathf.FloorToInt(eulerZ / 90) + Mathf.Round((eulerZ % 90) / 90));

        rotDuration = 1;
    }
}
