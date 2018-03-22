﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal1;
    public Transform portal2;
    
    void Update()
    {
        Vector3 playerDistanceToPortal = playerCamera.position - portal2.position;
        transform.position = portal1.position + playerDistanceToPortal;

        float PortalsAngle = Quaternion.Angle(portal1.rotation, portal2.rotation);

        Quaternion RotationDifference = Quaternion.AngleAxis(PortalsAngle, Vector3.up);
        Vector3 newCameraDirection = RotationDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}