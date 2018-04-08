using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBridge : MonoBehaviour {

    public GameObject blueFloor;

    private bool blueUp = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (blueUp)
        {
            blueFloor.SetActive(false);
        }
        else
        {
            blueFloor.SetActive(true);
        }
	}
}
