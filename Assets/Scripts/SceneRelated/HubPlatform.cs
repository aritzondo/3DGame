﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlatform : PlatformMovement {

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Player"))
        {
            currentWaypoint = 1;
            canMove = true;
            changeWp = false;
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
        {
            changeWp = true;
            toOrigin = true;
        }
    }

}
