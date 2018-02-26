using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *Hay que asegurarse de que detecta el objeto entero
 * Ahora lanza el ray al centro del objeto, por lo que no simpre lo detecta
 **/
public class lightTrigger : MonoBehaviour {

    public Light spotLight;
    public Material ilum;
    public Material notIlum;

    void OnTriggerEnter(Collider other)
    {
        //change the color of an illuminabel object to black
        RaycastHit hit;
        Vector3 direction = other.transform.position - spotLight.transform.position;
        Ray lightRay = new Ray(spotLight.transform.position, direction);
        if (Physics.Raycast(lightRay, out hit, spotLight.range))
        {
            //if the ray hits the same object the have enter in the trigger and it's illuminable, we change it's color
            if (hit.transform.gameObject.tag == "illuminable" && other.gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID())
            {
                other.GetComponent<Renderer>().material = ilum;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //change the color of an illuminable object to the default(red in this case)
        if (other.transform.gameObject.tag == "illuminable")
        {
            other.GetComponent<Renderer>().material = notIlum;
        }
    }
}
