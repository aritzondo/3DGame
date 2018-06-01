using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPad : MonoBehaviour {

    
    public GameObject tpPoint;
    public Transform cameraB;

    private GameObject player;
    private float dotPortals;
    private Transform Tplayer;

    private void Start()
    {
        player = AudioManager.GetInstance().player;
        Debug.Log(player);
        Tplayer = player.transform;
        dotPortals = Vector3.Dot(Tplayer.transform.forward, tpPoint.transform.forward);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Player")
        {
            Vector3 portalToPlayer = Tplayer.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            if (dotProduct < 0.0f)
            {
                Camera mainCamera = Camera.main;

                player = col.transform.gameObject;

                float myF = player.transform.eulerAngles.y + (tpPoint.transform.eulerAngles.y - transform.eulerAngles.y + 180.0f);

                float cameraOffset = player.transform.position.y - mainCamera.transform.position.y;
                
               player.transform.position = new Vector3(cameraB.position.x, cameraB.position.y+cameraOffset, cameraB.position.z);

                mainCamera.GetComponent<CameraMovement>().SetMouseLookX(myF);
            }
        }
        
    }
}
