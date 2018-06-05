using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickerMobile : MonoBehaviour {

    public Vector3 carryOffset = new Vector3(0,1,1);
    public Camera cam;
    public float maxDistance = 3.0f;

    private CharacterMovementMobile moveScript;

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
        moveScript = gameObject.GetComponent<CharacterMovementMobile>();
    }

    // When the mouse is pressed we throw a ray to the point were it has been clicked
    // and if its a rotator we activate it
    void Update () {
        if (!moveScript.interacting)
        {
            if (Input.touches.Length == 1)
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
                    if (Physics.Raycast(ray, out hit, maxDistance))
                    {
                        Movable moveScript = hit.transform.gameObject.GetComponent<Movable>();
                        if (moveScript != null && moveScript.isActiveAndEnabled)
                        {
                            Rotable rot = hit.transform.GetComponent<Rotable>();
                            if (rot != null && rot.isIdle())
                            {
                                moveScript.Carry(cam.transform, carryOffset);
                                carrying = true;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Cant drop");
                }
            }
            else if (Input.touches.Length == 2)
            {
                if (!carrying)
                {
                    //Cast a ray from the camera to his forward
                    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, maxDistance))
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
