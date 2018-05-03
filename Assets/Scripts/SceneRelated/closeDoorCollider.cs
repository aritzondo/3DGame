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
            door.GetComponent<DoorMovement>().startClosing();
            rotatorCube.GetComponent<MovingWithMouse>().doors.Add(door);
        }
    }
}
