using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicker : MonoBehaviour {

    public Vector3 carryOffset = new Vector3(0,1,1);
    public Camera cam;

    private CharacterMovement moveScript;

    private bool carrying = false;
    private bool canDrop = false;
    private DropTrigger dropSite;

    public DropTrigger DropSite
    {
        set { dropSite = value; }
    }

    public bool CanDrop
    {
        //get { return CanDrop; }
        set { canDrop = value; }
    }

    public bool Carrying
    {
        get { return carrying; }
    }

    private void Start()
    {
        moveScript = gameObject.GetComponent<CharacterMovement>();
    }

    // When the mouse is pressed we throw a ray to the point were it has been clicked
    // and if its a rotator we activate it
    void Update () {
        if (!moveScript.interacting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (carrying && canDrop)
                {
                    Movable carriedObj = GetComponentInChildren<Movable>();
                    carriedObj.hook = dropSite;
                    carriedObj.Release();
                    carrying = false;
                }
                else if (!carrying)
                {
                    //Cast a ray from the camera to his forward
                    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1.5f))
                    {
                        Movable moveScript = hit.transform.gameObject.GetComponent<Movable>();
                        if (moveScript != null && moveScript.isActiveAndEnabled)
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
                    if (Physics.Raycast(ray, out hit, 1.5f))
                    {
                        Rotable rotateScript = hit.transform.gameObject.GetComponent<Rotable>();
                        if (rotateScript != null && rotateScript.isActiveAndEnabled)
                        {
                            rotateScript.clicked();
                        }
                    }
                }
            }
        }
    }
}
