using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour {

    public Vector3 carryOffset = new Vector3(0,1,1);
    public Camera cam;

    private bool carrying = false;
    private bool canDrop = false;
    private Transform dropSite;

    public Transform DropSite
    {
        set { dropSite = value; }
    }

    public bool CanDrop
    {
        //get { return CanDrop; }
        set { canDrop = value; }
    }

    
	// When the mouse is pressed we throw a ray to the point were it has been clicked
    // and if its a rotator we activate it
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (carrying && canDrop)
            {
                GetComponentInChildren<Movable>().Release(dropSite);
                carrying = false;
            }
            else if(!carrying)
            {
                //Cast a ray from the camera to his forward
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                   Movable moveScript = hit.transform.gameObject.GetComponent<Movable>();
                    if (moveScript != null)
                    {
                        moveScript.Carry(cam.transform, carryOffset);
                        carrying = true;
                    }
                }
            }
            else
            {
                Debug.Log("Cant drop");
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (!carrying)
            {
                //Cast a ray from the camera to his forward
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                    Rotable movingScript = hit.transform.gameObject.GetComponent<Rotable>();
                    if (movingScript != null)
                    {
                        movingScript.clicked();
                    }
                }
            }
        }
    }
}
