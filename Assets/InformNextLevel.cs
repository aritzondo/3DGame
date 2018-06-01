using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformNextLevel : MonoBehaviour {

    public DoorMovement mDoor;

    private int nextLevel;

	// Use this for initialization
	void Start () {
        nextLevel = mDoor.sceneIndex;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DoorFrameManager.GetInstance().NewLevel(nextLevel);
            Debug.Log(nextLevel);
        }
    }
}
