using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLightActive : MonoBehaviour {

    public LightManager lManager;

    private Light mLight;

    private void Start()
    {
        mLight = gameObject.GetComponent<Light>();   
    }

    public void Activate()
    {
        lManager.activateLight(mLight);
    }

    public void Deactivate()
    {
        lManager.deactivateLight(mLight);
    }
}
