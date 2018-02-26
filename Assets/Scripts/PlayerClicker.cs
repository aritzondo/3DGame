using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour {

    public Camera cam;
    
	// When the mouse is pressed we throw a ray to the point were it has been clicked
    // and if its a rotator we activate it
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //Cast a ray from the camera to his forward
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 200.0f))
            {
                //if the ray hits a rotable object we call to his method to activate it
                if (hit.transform.gameObject.tag == "rotable")
                {
                    Debug.Log("hitted");
                    hit.transform.gameObject.GetComponent<movingWithMouse>().clicked();
                }
            }
        }
    }
}
