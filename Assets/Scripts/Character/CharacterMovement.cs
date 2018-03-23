﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    //public variables
	public float speed = 0.0f;
	public float jumpSpeed = 8.0f;
	public float gravity = 9.8f;
    public bool interacting = false;

    //private variables
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;


    void Start () {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if (controller.isGrounded)
        {
            //if the controller is interacting with something it cannot move
            if (!interacting)
            {
                /*
                 * use the horizontal and vertical axis to the movedirecton
                 * if press space -> jump
                 */
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    moveDirection.y = jumpSpeed;
                }

            }
            //block movement when interacting
            else
            {
                moveDirection = new Vector3(0, 0, 0);
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