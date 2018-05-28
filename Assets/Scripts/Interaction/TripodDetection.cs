using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodDetection : CameraDetection {
    /*
     *check if it in sight and inform the trigger for the drop of the lights
     */

    public DropTrigger trigger;

    protected override void Start()
    {
        base.Start();
    }

    protected override void inSight()
    {
        base.inSight();
        trigger.InSight = true;
    }

    protected override void notInSight()
    {
        base.notInSight();
        trigger.InSight = false;
    }
}
