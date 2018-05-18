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
        transform.localRotation = Quaternion.identity;
        Light light = GetComponentInChildren<Light>();
        if(light != null)
        {
            light.enabled = false;
        }
    }

    public void Release(Transform dropPoint)
    {
        transform.parent = null;
        transform.position = dropPoint.position;
        transform.rotation = dropPoint.rotation;
        Light light = GetComponentInChildren<Light>();
        if (light != null)
        {
            light.enabled = true;
        }
    }
}
