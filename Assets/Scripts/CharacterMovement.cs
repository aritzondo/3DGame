using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float speed = 0.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 9.81F;
	public float accel = 2.0F;
	public float maxSpeed = 4.0F;
	private bool lockCursor = true;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 airDirection = Vector3.zero;
    public bool interacting = false;
    CharacterController controller;


    void Start () {
		Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            if (!interacting)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                if (speed < maxSpeed && (moveDirection.x != 0 || moveDirection.z != 0))
                    speed = speed + accel * Time.deltaTime;
                if (moveDirection.x == 0 && moveDirection.z == 0 && speed >= 0)
                    speed = 2;
                //This has no use: speed = speed - accel*Time.deltaTime;
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }
            else
            {
                moveDirection = new Vector3(0, 0, 0);
            }


            if (Input.GetKeyDown(KeyCode.Space))
            { //if (Input.GetButton ("Jump"))
                Debug.Log("Grounded and press space");
                moveDirection.y = jumpSpeed;
            }

        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
       


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
        }
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void SetInteracting(bool newI)
    {
        interacting = newI;
    }
}