using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithMouse : Rotable
{
    //public attributes
    public GameObject hub;
    public float time90degrees = 1.0f;
    public GameObject openTrigger; //we disable the triggers while moving
    public GameObject closeTrigger;
    public ArrayList doors;

    private void Start()
    {
        cubeIniRot = transform.rotation;
        doors = new ArrayList(4);
        player = AudioManager.GetInstance().player.GetComponent<CharacterMovement>();
    }

    protected override void FixedUpdate()
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
                    openTrigger.GetComponent<Collider>().enabled = true;
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
                if (!Input.GetMouseButton(1))
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
    public override void clicked()
    {
        if (state == State.Idle || state == State.Adjusting)
        {
            Camera.main.GetComponent<CameraMovement>().enabled = false;
            player.GetComponent<CharacterMovement>().SetInteracting(true);
            doors.Clear();
            closeTrigger.GetComponent<MeshCollider>().enabled = true;
            openTrigger.GetComponent<Collider>().enabled = false;
            state = State.Clicked;
        }
    }

    //when the mouse is released we give back the control to the player and start the adjusting proccess
    public override void released()
    {
        base.released();

        closeTrigger.GetComponent<MeshCollider>().enabled = false;
    }

    private void startRotationWithHub()
    {
        openTrigger.GetComponent<Collider>().enabled = false;

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