using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedCheck : MonoBehaviour {

    public Material finishedColor;

    public void FinishedLevel()
    {
        GetComponent<Renderer>().material = finishedColor;
    }
}
