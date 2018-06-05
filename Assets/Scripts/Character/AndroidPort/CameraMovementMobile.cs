using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementMobile : MonoBehaviour {

    //Vector2 mouseLook;
    float mouseLook;
    float smoothV;
	public float sensitivity = 5.0f;
	public float smoothing   = 2.0f;

	GameObject character;

	void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        character = this.transform.parent.gameObject;
	}

    void Update()
    {
        //mouse delta
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float md = Input.acceleration.x * smoothing * sensitivity;
            smoothV = Mathf.Lerp(smoothV, md, 1f / smoothing);
            mouseLook += smoothV;
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook, character.transform.up);
        }
	}
    public void SetMouseLookX(float newLookX)
    {
        mouseLook = newLookX;
    }
}

