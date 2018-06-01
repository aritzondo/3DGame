using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedCheck : MonoBehaviour {

    public Material finishedColor;
    public Material closedDoor;

    public void ClosedLevel()
    {
        GetComponent<Renderer>().material = closedDoor;
    }

    public void FinishedLevel()
    {
        GetComponent<Renderer>().material = finishedColor;
    }
}
