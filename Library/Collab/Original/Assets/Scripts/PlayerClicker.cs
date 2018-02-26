using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour {
    private Camera myCamera;
	// Use this for initialization
	void Start () {
        myCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(myCamera.transform.position, myCamera.transform.forward);
        //Debug.Log(myCamera.transform.position+","+myCamera.transform.forward);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(myCamera.transform.position,myCamera.transform.forward, out hit))
            {
                
                //Debug.Log(hit.transform.name);

                if (hit.transform.gameObject.tag == "rotable")
                {
                    hit.transform.gameObject.GetComponent<movingWithMouse>().clicked();
                }
            }
        }
	}
}
