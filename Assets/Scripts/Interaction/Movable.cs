using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Carry(Transform parent, Vector3 offset)
    {
        transform.SetParent(parent);
        transform.localPosition = offset;
    }

    public void Release()
    {
        transform.parent = null;
    }
}
