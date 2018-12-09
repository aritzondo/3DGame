using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorCollider : MonoBehaviour {

    public MovingWithMouse rotatorCube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("door"))
        {
            GameObject door = other.gameObject;
            DoorMovement dMove = door.GetComponent<DoorMovement>();
            dMove.startClosing();
            rotatorCube.doors.Add(door);

            StartCoroutine(dMove.UnloadAsyncScene());

        }
    }
}
