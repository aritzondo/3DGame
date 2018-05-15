using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour {

    public Vector3 carryOffset = new Vector3(0,1,1);
    public Camera cam;

    private bool carrying = false;
    
	// When the mouse is pressed we throw a ray to the point were it has been clicked
    // and if its a rotator we activate it
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (carrying)
            {
                GetComponentInChildren<Movable>().Release();
                carrying = false;
            }
            else
            {
                //Cast a ray from the camera to his forward
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 10.0f))
                {
                    MovingWithMouse movingScript = hit.transform.gameObject.GetComponent<MovingWithMouse>();
                    if (movingScript != null)
                    {
                        movingScript.clicked();
                    }

                    Movable moveScript = hit.transform.gameObject.GetComponent<Movable>();
                    if (moveScript != null)
                    {
                        moveScript.Carry(transform, carryOffset);
                        carrying = true;
                    }
                }
            }
        }
    }
}
