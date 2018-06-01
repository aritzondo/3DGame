using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedCheck : MonoBehaviour {

    public Material finishedColor;

    public void FinishedLevel()
    {
        GetComponent<Renderer>().material = finishedColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("He entrado en el trigger de los cojones");
            FinishedLevel();
        }
    }
}
