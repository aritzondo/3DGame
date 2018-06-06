using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementMobile : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    GameObject character;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        float inputX;
        float inputY;
        //mouse delta
        if (Cursor.lockState == CursorLockMode.Locked)
        {

            if (mouseLook.y >= -90.0f && mouseLook.y <= 90.0f)
            {
                switch (SystemInfo.deviceType)
                {
                    case DeviceType.Desktop | DeviceType.Console:
                        {
                            inputX = Input.GetAxisRaw("Mouse X");
                            inputY = Input.GetAxisRaw("Mouse Y");
                            break;
                        }
                    default:
                        {
                            inputX = Input.acceleration.x;
                            inputY = 0.0f;
                            break;
                        }
                }
                var md = new Vector2(inputX,inputY);
                md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
                smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
                smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
                mouseLook += smoothV;
                transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
                character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

            }
            else
            {
                mouseLook.y = 90.0f * Mathf.Sign(mouseLook.y);
            }
        }
    }
    public void SetMouseLookX(float newLookX)
    {
        mouseLook.x = newLookX;
    }
}

