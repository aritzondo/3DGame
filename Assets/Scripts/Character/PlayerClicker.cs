﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour
{
    //PC
    public Vector3 carryOffset = new Vector3(0, 1, 1);
    public Camera cam;
    public float maxDistance = 3.0f;

    //PC
    private CharacterMovement moveScript;
    private bool carrying = false;
    private bool canDrop = false;
    private DropTrigger dropSite;
    //Android
    private float startTime;
    private float endTime;
    private float swipeTime;
    private float minTimeToBeSwipe = 0.1f;
    private Vector3 startPos;
    private Vector3 endPos;
    private float swipeDistance;

    public DropTrigger DropSite
    {
        set { dropSite = value; }
    }

    public bool CanDrop
    {
        //get { return CanDrop; }
        set { canDrop = value; }
    }

    public bool Carrying
    {
        get { return carrying; }
    }

    private void Start()
    {
        moveScript = gameObject.GetComponent<CharacterMovement>();
    }

    // When the mouse is pressed we throw a ray to the point were it has been clicked
    // and if its a rotator we activate it
    void Update()
    {
        switch (SystemInfo.deviceType)
        {
            case DeviceType.Desktop | DeviceType.Console:
                {
                    PC();
                    break;
                }
            default:
                {
                    Android();
                    break;
                }
        }
    }

    private void PC()
    {
        if (!moveScript.interacting)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button4))
            {
                if (carrying && canDrop)
                {
                    Movable carriedObj = GetComponentInChildren<Movable>();
                    carriedObj.hook = dropSite;
                    carriedObj.Release();
                    carrying = false;
                }
                else if (!carrying)
                {
                    //Cast a ray from the camera to his forward
                    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, maxDistance))
                    {
                        Movable moveScript = hit.transform.gameObject.GetComponent<Movable>();
                        if (moveScript != null && moveScript.isActiveAndEnabled)
                        {
                            Rotable rot = hit.transform.GetComponent<Rotable>();
                            if (rot != null && rot.isIdle())
                            {
                                moveScript.Carry(cam.transform, carryOffset);
                                carrying = true;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Cant drop");
                }
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                if (!carrying)
                {
                    //Cast a ray from the camera to his forward
                    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, maxDistance))
                    {
                        Rotable rotateScript = hit.transform.gameObject.GetComponent<Rotable>();
                        if (rotateScript != null && rotateScript.isActiveAndEnabled)
                        {
                            rotateScript.clicked();
                        }
                    }
                }
            }
        }
    }

    private void Android()
    {
        if (Input.touchCount > 0)
        {
            float dt = Time.deltaTime;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = touch.position;

                swipeDistance = (endPos - startPos).magnitude;
                swipeTime = (endTime - startTime);

                if (swipeTime <= minTimeToBeSwipe)
                {
                    if (carrying && canDrop)
                    {
                        Movable carriedObj = GetComponentInChildren<Movable>();
                        carriedObj.hook = dropSite;
                        carriedObj.Release();
                        carrying = false;
                    }
                    else if (!carrying)
                    {
                        //Cast a ray from the camera to his forward
                        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxDistance))
                        {
                            Movable moveScript = hit.transform.gameObject.GetComponent<Movable>();
                            if (moveScript != null && moveScript.isActiveAndEnabled)
                            {
                                Rotable rot = hit.transform.GetComponent<Rotable>();
                                if (rot != null && rot.isIdle())
                                {
                                    moveScript.Carry(cam.transform, carryOffset);
                                    carrying = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Cant drop");
                    }
                }
            }

            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                swipeTime = (Time.time - startTime);
                if (swipeTime > minTimeToBeSwipe)
                {
                    if (!carrying)
                    {
                        //Cast a ray from the camera to his forward
                        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, maxDistance))
                        {
                            Rotable rotateScript = hit.transform.gameObject.GetComponent<Rotable>();
                            if (rotateScript != null && rotateScript.isActiveAndEnabled)
                            {
                                rotateScript.clickedAndroid(touch);
                            }
                        }
                    }
                }
            }
        }
    }
}