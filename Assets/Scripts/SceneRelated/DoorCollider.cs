using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour {

    public GameObject door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("door"))
        {
            door = other.gameObject;
            Debug.Log("Door is opening");
            //door.GetComponent<DoorMovement>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("door"))
        {
            door = other.gameObject;
            Debug.Log("Door is closing");
            //door.GetComponent<DoorMovement>();
        }
    }
}
