using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform portal1;
    public Transform portal2;
    
    private float dotPortals;
    private Camera m_camera;

    void Start()
    {
        dotPortals = Vector3.Dot(portal1.transform.forward, portal2.transform.forward);
        m_camera = Camera.main;
    }


    void LateUpdate()
    {
        
        Vector3 playerDistanceToPortal = m_camera.transform.position - portal2.position;

        Vector3 newPos = transform.position;
        newPos.y = portal1.position.y + playerDistanceToPortal.y;

        if (dotPortals < 0)
        {
            newPos.x = portal1.position.x - playerDistanceToPortal.x;
            newPos.z = portal1.position.z - playerDistanceToPortal.z;
        }
        else
        {
            newPos.x = portal1.position.x + playerDistanceToPortal.x;
            newPos.z = portal1.position.z + playerDistanceToPortal.z;
        }
        transform.position = newPos;
        
        float PortalsAngle = Quaternion.Angle(portal1.rotation, portal2.rotation);

        Quaternion rotationDifference = Quaternion.AngleAxis(PortalsAngle, Vector3.up);
        Vector3 newCameraDirection = rotationDifference * m_camera.transform.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }
}
