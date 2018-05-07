using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour {

   private Renderer m_renderer;
    private Camera cam;
    
    private bool trigger = false;

    // Use this for initialization
    void Start () {
        m_renderer = GetComponent<Renderer>();
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        if (m_renderer.isVisible)
        {
            if (inCamera()) inSight();
        }
        else
        {
            notInSight();
        }
    }

    private bool inCamera()
    {
        Ray ray = new Ray(cam.transform.position, transform.position - Camera.main.transform.position);
        RaycastHit hit;
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

    private void inSight()
    {
        if (!trigger)
        {
            Debug.Log(gameObject.name + " detected!");
            trigger = true;
        }
        
    }

    private void notInSight()
    {
        if (trigger)
        {
            Debug.Log("not in sight");
            trigger = false;
        }
    }
}
