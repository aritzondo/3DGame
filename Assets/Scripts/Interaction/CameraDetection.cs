using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour {
    /**
     * Class that check if the object is in sight of the camera
     * It has two virtual methods that should be overriden to give the desire behaviour
     */

   private Renderer m_renderer;
    private Camera cam;
    
    private bool visible = false;

    // Use this for initialization
    protected virtual void Start () {
        m_renderer = GetComponent<Renderer>();
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        //if the render isVisible to any camera that is rendering 
        if (m_renderer.isVisible)
        {
            if (inCamera()) inSight();
        }
        else
        {
            notInSight();
        }
    }

    //cast a ray from the camera to the object to test if there is an obstacle in the middle
    private bool inCamera()
    {
        Ray ray = new Ray(cam.transform.position, transform.position - Camera.main.transform.position);
        RaycastHit hit;
        Debug.DrawRay(ray.origin,ray.direction*10);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.Equals(this.gameObject))
            {
                return true;
            }
            else return false;
        }

        return false;
    }

    protected virtual void inSight()
    {
        if (!visible)
        {
            Debug.Log(gameObject.name + " detected!");
            visible = true;
        }
        
    }

    protected virtual void notInSight()
    {
        if (visible)
        {
            Debug.Log("not in sight");
            visible = false;
        }
    }
}
