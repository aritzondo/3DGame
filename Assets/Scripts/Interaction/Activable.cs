using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour {

    public float timeToActivate;

    protected float timeInLight = 0.0f;
    protected bool inLight = false;
    protected bool activated = false;
    protected int lightsDetected = 0;

    public int LightsDetected { get { return lightsDetected; } }
    public bool InLight { get { return inLight; } }

    protected void Update()
    {
        if (inLight)
        {
            timeInLight += Time.deltaTime;
            if(timeInLight >= timeToActivate && !activated)
            {
                activated = true;
                Activate();
            }
        }
    }

    //this function should be override in each class
    public virtual void Activate()
    {

    }

    public virtual void enterInLight()
    {
        timeInLight += Time.deltaTime;
        inLight = true;
        lightsDetected++;
    }

    public virtual void exitLight()
    {
        lightsDetected--;
        if (lightsDetected <= 0)
        {
            timeInLight = 0.0f;
            inLight = false;
            activated = false;
        }
    }
}
