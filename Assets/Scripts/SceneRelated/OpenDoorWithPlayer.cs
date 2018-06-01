using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorWithPlayer : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            door.GetComponent<DoorMovement>().startMovement();
        }
    }
}
