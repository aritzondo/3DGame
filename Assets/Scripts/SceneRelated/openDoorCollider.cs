using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "door" && gameObject.GetComponent<MeshCollider>().enabled)
        {
            GameObject door = other.gameObject;
            DoorMovement dMove = door.GetComponent<DoorMovement>();
            dMove.resetIniPos();
            dMove.startMovement();

            StartCoroutine(dMove.LoadAsyncScene());
            
        }
    }
}
