using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if(Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(ray.origin,ray.direction);

                if (hit.transform.gameObject.tag == "rotable")
                {
                    hit.transform.gameObject.GetComponent<movingWithMouse>().clicked();
                }
            }
        }
	}
}
