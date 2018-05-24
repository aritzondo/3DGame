using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {

    private Light mLight;

	// Use this for initialization
	void Start () {
        mLight = gameObject.GetComponentInChildren<Light>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Carry(Transform parent, Vector3 offset)
    {
        transform.SetParent(parent);
        transform.localPosition = offset;
        transform.localRotation = Quaternion.identity;
        if(mLight != null)
        {
            CheckLightActive checkScript = GetComponentInChildren<CheckLightActive>();
            if (checkScript != null)
            {
                checkScript.Deactivate();
            }
            else
            {
                mLight.enabled = false;
            }
        }
    }

    public void Release(Transform dropPoint)
    {
        transform.parent = null;
        transform.position = dropPoint.position;
        transform.rotation = dropPoint.rotation;
        CheckLightActive checkScript = GetComponentInChildren<CheckLightActive>();
        if (checkScript != null)
        {
            checkScript.Activate();
        }
        else
        {
            mLight.enabled = true;
        }
    }
}
