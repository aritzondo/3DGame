using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTrigger : MonoBehaviour {

    public Light spotLight;

    private void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        drawRayToLighted(other);
    }
    void OnTriggerStay(Collider other)
    {
        drawRayToLighted(other);
    }

    void drawRayToLighted(Collider other)
    {
        RaycastHit hit;
        Vector3 direction = other.transform.position-transform.position;
        Ray lightRay = new Ray(transform.position, direction);
        if (Physics.Raycast(lightRay, out hit, spotLight.range))
            if (hit.transform.gameObject.tag == "illuminable" && other.gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID())
            {
                Debug.DrawRay(transform.position, direction * spotLight.range);
                Debug.Log(hit.transform.gameObject);
            }
    }
}
