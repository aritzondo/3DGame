﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementMobile : MonoBehaviour {
    //public variables
	public float speed = 5.0f;
	public float jumpSpeed = 5.0f;
	public float gravity = 9.8f;
    public bool interacting = false;
    public float walkSoundTime = 0.6f;

    //private variables
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
    private AudioManager audioManager;
    private float countToWalkSound = 0.0f;
    private bool alternateWalkSound = true;

    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        controller = GetComponent<CharacterController>();
        audioManager = AudioManager.GetInstance();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        float inputX;
        float inputZ;
        if (controller.isGrounded)
        {
            //if the controller is interacting with something it cannot move
            if (!interacting)
            {
                /*
                 * use the horizontal and vertical axis to the movedirecton
                 * if press space -> jump
                 */
                switch(SystemInfo.deviceType)
                {
                    case DeviceType.Desktop | DeviceType.Console:
                        {
                            inputX = Input.GetAxisRaw("Horizontal");
                            inputZ = Input.GetAxisRaw("Vertical");
                            break;
                        }
                    default:
                        {
                            inputX = 0.0f;
                            inputZ = -Input.acceleration.z;
                            break;
                        }
                }
                moveDirection = new Vector3(inputX, 0.0f, inputZ);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (Input.touchCount == 2 || Input.GetAxisRaw("Jump")!=0)
                {
                    moveDirection.y = jumpSpeed;
                }

            }
            //block movement when interacting
            else
            {
                moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
            }

            countToWalkSound += dt;
            if (countToWalkSound > walkSoundTime)
            {
                if (moveDirection.x != 0 || moveDirection.z != 0)
                {
                    countToWalkSound = 0.0f;
                    alternateWalkSound = !alternateWalkSound;
                    if (alternateWalkSound)
                        audioManager.Play((int)AudioManager.SoundGeneral.PASO1);
                    else
                        audioManager.Play((int)AudioManager.SoundGeneral.PASO2);
                }
            }
        }
        //apply gravity if you aren't grounded
        moveDirection.y -= gravity * dt;
        //apply the movement to the controller
        controller.Move(moveDirection * dt);
    }

    public void SetInteracting(bool newI)
    {
        interacting = newI;
    }
}