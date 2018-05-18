﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrigger : MonoBehaviour {
    /**
     * If the object that has this trigger is in sight of the camera and the player is in the trigger
     * it informs the player that he can drop the light and the position where it should drop it
     */

    public Transform dropPoint;

    private bool inSight = false;

    public bool InSight
    {
        set { inSight = value; }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerClicker clicker = other.gameObject.GetComponent<PlayerClicker>();
        if (clicker != null)
        {
            //if the player is in the trigger it will only be able to drop the light if it's in sight
            clicker.CanDrop = inSight;
            clicker.DropSite = dropPoint;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerClicker clicker = other.gameObject.GetComponent<PlayerClicker>();
        if (clicker != null)
        {
            clicker.CanDrop = false;
        }
    }
}