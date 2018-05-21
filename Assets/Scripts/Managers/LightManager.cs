using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    private static LightManager instance;

    public Light[] lights;
    public int activeLight = -1;
    public int defaultActiveLight = -1;

    public static LightManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Use this for initialization
    void Start () {
		for(int i = 0; i < lights.Length; i++)
        {
            if (i != activeLight)
            {
                lights[i].enabled = false;
            }
        }
        if(activeLight < 0 && defaultActiveLight >= 0 && defaultActiveLight <= lights.Length)
        {
            lights[defaultActiveLight].enabled = true;
            activeLight = defaultActiveLight;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void activateLight(Light newActive)
    {
        if(activeLight >= 0)
            lights[activeLight].enabled = false;

        for(int i = 0; i< lights.Length;i++)
        {
            if (newActive.Equals(lights[i]))
            {
                activeLight = i;
                newActive.enabled = true;
            }
        }
    }

    public void deactivateLight(Light light)
    {
        light.enabled = false;
        activeLight = defaultActiveLight;
        lights[activeLight].enabled = true;
    }
}
