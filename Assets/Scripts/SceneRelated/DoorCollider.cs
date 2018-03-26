using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "door")
        {
            GameObject door = other.gameObject;
            Debug.Log("Opening door "+other.gameObject.name);
            door.GetComponent<DoorMovement>().isOpening = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "door")
        {
            GameObject door = other.gameObject;
            Debug.Log("Door is closing");
            
            
        }
    }
}
