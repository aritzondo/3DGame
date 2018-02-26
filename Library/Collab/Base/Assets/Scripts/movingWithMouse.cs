using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingWithMouse : MonoBehaviour
{
    public CharacterMovement player;
    public rotateWithCube hub;
    public bool check = true;
    public float sensitivity = 15.0f;
    public float rotSpeed = 20;
    public bool activate = false;
    float rotX = 0.0f;
    float rotY = 0.0f;
    Quaternion initialState;
    //variables for the state of the rotator
    bool rotating = false;
    bool returning = false;
    Vector3 nextAngle = new Vector3(0, 0, 0);

    void Start()
    {
        initialState = transform.rotation;
        hub.iniCubeRot = initialState;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            rotating = false;
            rotX += Input.GetAxis("Mouse Y") * rotSpeed * sensitivity * Mathf.Deg2Rad;
            rotY -= Input.GetAxis("Mouse X") * rotSpeed * sensitivity * Mathf.Deg2Rad;
        }

        if (rotating || returning)
        {
            //calculo la siguiente rotacion
            Quaternion newRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(nextAngle.x, nextAngle.y, nextAngle.z), Time.deltaTime * 2);
            if ( newRot == transform.rotation)
            {
                if (returning)
                {
                    Debug.Log("End return");
                    returning = false;
                }
                if (rotating)
                {
                    Debug.Log("End adjust");
                    rotating = false;
                    returning = true;
                    nextAngle = new Vector3(0, 0, 0);
                }
            }
            else
            {
                transform.rotation = newRot;
                if (returning)
                {
                    hub.transform.rotation = newRot;
                }
            }
            initialState = transform.rotation;
        }
        else
        {
            //apply the rotation of the mouse
            transform.rotation = initialState;
            transform.Rotate(rotX, rotY, 0);
        }
    }

    //calculate the most proxim angle of rotation (right angles)
    public void adjustAngle()
    {
        float eulerX = transform.rotation.eulerAngles.x;
        float eulerY = transform.rotation.eulerAngles.y;
        float eulerZ = transform.rotation.eulerAngles.z;

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
        //
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
        Debug.Log("Giro x a " + nextAngle.x + ",  y a " + nextAngle.y +"y z a "+nextAngle.z+ ", con eulerX= " + eulerX + ", eulerY= " + eulerY+" y eulerZ= "+eulerZ);

    }

    public void clicked() 
    {
        activate = true;
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        Debug.Log("Click");
    }
    public void OnMouseUp()
    {
        rotX = 0;
        rotY = 0;
        adjustAngle();
        rotating = true;
        activate = false;
        Camera.main.GetComponent<CameraMovement>().enabled = true;
    }
}