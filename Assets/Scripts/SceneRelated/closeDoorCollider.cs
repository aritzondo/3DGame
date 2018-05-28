using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorCollider : MonoBehaviour {

    public GameObject rotatorCube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "door")
        {
            GameObject door = other.gameObject;
            DoorMovement dMove = door.GetComponent<DoorMovement>();
            dMove.startClosing();
            rotatorCube.GetComponent<MovingWithMouse>().doors.Add(door);

            StartCoroutine(dMove.UnloadAsyncScene());

        }
    }
}
