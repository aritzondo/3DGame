using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlatform : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        GetComponent<PlatformMovement>().ChangeCurrWaypoint = 1;
        GetComponent<PlatformMovement>().CanMove = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<PlatformMovement>().ChangeWp = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<PlatformMovement>().ChangeWp = true;
            GetComponent<PlatformMovement>().Return = true;
        }
    }

}
