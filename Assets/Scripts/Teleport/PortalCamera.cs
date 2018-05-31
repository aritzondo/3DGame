using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal1;
    public Transform portal2;
    
    private float dotPortals;

    void Start()
    {
        dotPortals = Vector3.Dot(portal1.transform.forward, portal2.transform.forward);
    }


    void LateUpdate()
    {
        
        Vector3 playerDistanceToPortal = playerCamera.position - portal2.position;
        if(dotPortals < 0)
        {
            transform.position = portal1.position - playerDistanceToPortal;
        }
        else
        {
            transform.position = portal1.position + playerDistanceToPortal;
        }
        
        float PortalsAngle = Quaternion.Angle(portal1.rotation, portal2.rotation);

        Quaternion RotationDifference = Quaternion.AngleAxis(PortalsAngle, Vector3.up);
        Vector3 newCameraDirection = RotationDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }
}
