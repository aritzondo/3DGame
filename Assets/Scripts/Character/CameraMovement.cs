using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	//@Incomplete - Recode everything when you learn to C# and Unity, 
	//this needs to be changed. I don't understand half of the things 
	//here because I copy-pasted from a tutorial and the clamping 
	//up-down is awful but it works for prototyping. jChue 08/2017

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing   = 2.0f;

	GameObject character;

	void Start () {
		character = this.transform.parent.gameObject;
	}

	void Update () {
		//mouse delta
		if(Cursor.lockState == CursorLockMode.Locked){
			
			if (mouseLook.y >= -90.0f && mouseLook.y <= 90.0f) {
				var md = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
				md = Vector2.Scale (md, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
				smoothV.x = Mathf.Lerp (smoothV.x, md.x, 1f / smoothing);
				smoothV.y = Mathf.Lerp (smoothV.y, md.y, 1f / smoothing);
				mouseLook += smoothV;
				transform.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right);
				character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);
				
			} else {
				mouseLook.y = 90.0f * Mathf.Sign(mouseLook.y);
			}
        }
	}
    public void SetMouseLookX(float newLookX)
    {
        mouseLook.x = newLookX;
    }
}

