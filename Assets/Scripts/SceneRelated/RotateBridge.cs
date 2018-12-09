using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBridge : MonoBehaviour {

    public int bridgeNumberOfFaces = 4;
    public float rotDuration = 5.0f;   //duration of the rotation
    public float differenceAngle = 90.0f;
    public char axis = 'X';

    private float rotTime = 0.0f;   //time rotating
    private Quaternion[] rotations;

    private State state = State.Idle;

    public enum State
    {
        Idle,
        Rotating
    }

    public RotateBridge()
    {
        CurrentRot = 0;
        Trigger = false;
    }

    public int CurrentRot { get; set; }

    public bool Trigger { get; set; }


    // Use this for initialization
    void Start() {
        rotations = new Quaternion[bridgeNumberOfFaces];
        rotations[0] = transform.rotation;

        switch (axis)
        {
            case 'X':
                for (int i = 1; i < bridgeNumberOfFaces; ++i)
                {
                    rotations[i] = rotations[i - 1] * Quaternion.Euler(differenceAngle, 0.0f, 0.0f);
                }
                break;
            case 'Y':
                for (int i = 1; i < bridgeNumberOfFaces; ++i)
                {
                    rotations[i] = rotations[i - 1] * Quaternion.Euler(0.0f, differenceAngle, 0.0f);
                }
                break;
            default:
                for (int i = 1; i < bridgeNumberOfFaces; ++i)
                {
                    rotations[i] = rotations[i - 1] * Quaternion.Euler(0.0f, 0.0f, differenceAngle);
                }
                break;
        }
    
    }
	
	// Update is called once per frame
    private void Update () {
        if (!Trigger) return;
        
        rotTime = 0.0f;
        CurrentRot = (CurrentRot + 1) % bridgeNumberOfFaces;
        state = State.Rotating;
        Trigger = false;
    }

    private void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        //if the mouse has been released
        switch (state)
        {
            case (State.Idle):
                //nothing
                break;
            case (State.Rotating):  //returning to base rotation dragin the hub
                //calculate the next rotation with an interpolation
                rotTime += dt;
                float t = rotTime / rotDuration;
                Quaternion newRot = Quaternion.Slerp(transform.localRotation, rotations[CurrentRot], t);
                if (Quaternion.Angle(transform.localRotation, newRot) <= Mathf.Epsilon)
                {
                    transform.localRotation = rotations[CurrentRot];
                    //if it was returning we end the rotation and set the time of the next rotation to the adjust time
                    state = State.Idle;
                }
                else
                {
                    transform.localRotation = newRot;
                }
                break;
        }
    }
}
