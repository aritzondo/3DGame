using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour {

    public float timeToActivate;

    protected float timeInLight = 0.0f;
    protected bool active = false;

    protected void Update()
    {
        if (active)
        {
            timeInLight += Time.deltaTime;
            if(timeInLight >= timeToActivate)
            {
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
        active = true;
    }

    public virtual void exitLight()
    {
        timeInLight = 0.0f;
        active = false;
    }
}
