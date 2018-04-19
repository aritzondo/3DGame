using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "door" && gameObject.GetComponent<MeshCollider>().enabled)
        {
            GameObject door = other.gameObject;
            door.GetComponent<DoorMovement>().startMovement();
            
        }
    }
}
