using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour {
    public float maxspeed = 1.5f;
    public float accel = 20.0f;
    private Vector3 initialPosition;
    private Vector3 desiredPosition;
    public bool isOpening = false;
    public bool isClosing = false;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        float dt = Time.deltaTime;

        if (isOpening)
        {
            if (desiredPosition.y >= transform.position.y)
            {
                transform.position += Vector3.up * maxspeed * dt;
            }
            else
            {
                isOpening = false;
            }
            
            
        }

        if(isClosing==true)
        {

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "doorcol")
        {
            initialPosition = transform.position;
            transform.position += Vector3.forward * 2.0f * Time.deltaTime;
            desiredPosition = transform.position;
            desiredPosition.y = initialPosition.y + 10.0f;
            
        }
    }
}
