using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithMouse : MonoBehaviour
{
    //public attributes
    public CharacterMovement player;
    public GameObject hub;
    public float sensitivity = 100.0f;   //sensitivity of the rotation with the mouse
    public float time90degrees = 1.0f;
    public GameObject openTrigger; //we disable the triggers while moving
    public GameObject closeTrigger;
    public ArrayList doors;

    //private attributes
    private float rotX = 0.0f;  //the ammount of rotation on x
    private float rotY = 0.0f;  //the ammount of rotation on y
    //variables for the state of the rotator
    private Vector3 nextAngle = new Vector3(0, 0, 0);
    private Quaternion cubeIniRot;
    private float rotTime = 0.0f;   //time rotating
    private float rotDuration = 0.0f;   //duration of the rotation
    private State state = State.Idle;

    enum State
    {
        Clicked,
        Idle,
        Adjusting,
        Waiting,
        Rotating
    }

    void Start()
    {
        cubeIniRot = transform.rotation;
        doors = new ArrayList(4);
    }

    void FixedUpdate()
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
            case (State.Waiting):   //waiting to rotate until the doors are closed
                if (checkDoorsClosed())
                {
                    state = State.Rotating;
                }
                break;
            case (State.Adjusting): //going to a "square" angle
                rotTime += dt;
                t = rotTime / rotDuration;
                newRot = Quaternion.Slerp(cubeIniRot, Quaternion.Euler(nextAngle), t);
                if (Quaternion.Angle(transform.rotation, newRot) <= Mathf.Epsilon)
                {
                    transform.rotation = Quaternion.Euler(nextAngle);
                    startRotationWithHub();
                }
                else
                {
                    transform.rotation = newRot;
                }
                break;
            case (State.Rotating):  //returning to base rotation dragin the hub
                //calculate the next rotation with an interpolation
                rotTime += dt;
                t = rotTime / rotDuration;
                newRot = Quaternion.Slerp(cubeIniRot, Quaternion.Euler(nextAngle), t);
                if (Quaternion.Angle(transform.rotation, newRot) <= Mathf.Epsilon)
                {
                    //if it was returning we end the rotation and set the time of the next rotation to the adjust time
                    state = State.Idle;
                    openTrigger.GetComponent<MeshCollider>().enabled = true;
                }
                else
                {
                    Quaternion offset = newRot * Quaternion.Inverse(transform.rotation);
                    hub.transform.Rotate(offset.eulerAngles, Space.World);
                    transform.rotation = newRot;
                }
                break;
            case (State.Clicked):   //rotated by the player
                //if the mouse has been released
                if (Input.GetMouseButtonUp(0))
                {
                    released();
                    break;
                }
                rotX += Input.GetAxis("Mouse Y") * sensitivity * dt;
                rotY -= Input.GetAxis("Mouse X") * sensitivity * dt;
                //apply the rotation of the mouse
                transform.rotation = Quaternion.Euler(rotX, rotY, 0);
                break;
        }
    }

    //when the cube is clicked the camera and movement of the player are blocked and the cube is activated
    public void clicked()
    {
        if (state == State.Idle || state == State.Adjusting)
        {
            Camera.main.GetComponent<CameraMovement>().enabled = false;
            player.GetComponent<CharacterMovement>().SetInteracting(true);
            doors.Clear();
            closeTrigger.GetComponent<MeshCollider>().enabled = true;
            openTrigger.GetComponent<MeshCollider>().enabled = false;
            state = State.Clicked;
        }
    }

    //when the mouse is released we give back the control to the player and start the adjusting proccess
    public void released()
    {
        rotX = 0;
        rotY = 0;
        adjustAngle();
        state = State.Adjusting;
        rotTime = 0;
        cubeIniRot = transform.rotation;

        Camera.main.GetComponent<CameraMovement>().enabled = true;
        player.GetComponent<CharacterMovement>().SetInteracting(false);

        closeTrigger.GetComponent<MeshCollider>().enabled = false;
        //openTrigger.GetComponent<MeshCollider>().enabled = true;
    }

    //calculate the closest angle of rotation (right angles)
    public void adjustAngle()
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


    private void startRotationWithHub()
    {
        openTrigger.GetComponent<MeshCollider>().enabled = false;

        if (checkDoorsClosed())
        {
            state = State.Rotating;
        }
        else
        {
            state = State.Waiting;
        }

        cubeIniRot = transform.rotation;
        rotTime = 0;
        nextAngle = new Vector3(0, 0, 0);
        float angleBetween = Quaternion.Angle(transform.rotation, Quaternion.Euler(nextAngle));
        rotDuration = (angleBetween / 90) * time90degrees;

        openTrigger.GetComponent<MeshCollider>().enabled = false;
    }

    private bool checkDoorsClosed()
    {
        foreach (GameObject door in doors)
        {
            if (!door.GetComponent<DoorMovement>().isClosed())
            {
                return false;
            }
        }
        return true;
    }
}