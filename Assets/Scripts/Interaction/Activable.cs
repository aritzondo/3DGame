using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour {

    public float timeToActivate;

    private float timeInLight = 0.0f;
    private bool active = false;

    private void Update()
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

    public void enterInLight()
    {
        timeInLight += Time.deltaTime;
        active = true;
    }

    public void exitLight()
    {
        timeInLight = 0.0f;
        active = false;
    }
}
