using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLightActive : MonoBehaviour {

    public LightManager lManager;
    public Material white;
    public Material black;
    public GameObject lens;

    private Light mLight;

    private void Start()
    {
        mLight = gameObject.GetComponent<Light>();
        if (mLight.isActiveAndEnabled)
        {
            lens.GetComponent<Renderer>().material = white;
        }
        else
        {
            lens.GetComponent<Renderer>().material = black;
        }
    }

    public void Activate()
    {
        if (lManager != null)
        {
            lManager.activateLight(mLight);
            lens.GetComponent<Renderer>().material = white;
        }
    }

    public void Deactivate()
    {
        if (lManager != null)
        {
            lManager.deactivateLight(mLight);
            lens.GetComponent<Renderer>().material = black;
        }
    }
}
