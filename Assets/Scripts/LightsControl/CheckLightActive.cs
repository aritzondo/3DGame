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
        if(lManager != null)
            lManager.activateLight(mLight);
    }

    public void Deactivate()
    {
        if (lManager != null)
            lManager.deactivateLight(mLight);
    }
}
