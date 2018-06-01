using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPad : MonoBehaviour {
    
    public GameObject tpPoint;
    public Transform cameraB;

    private GameObject player;
    private float dotPortals;
    private Transform Tplayer;
    private LevelsMusicManager musicManager;
    //private DoorFrameManager frameManager;

    private void Start()
    {
        musicManager = AudioManager.GetInstance().musicManager;
        player = AudioManager.GetInstance().player;
        Debug.Log(player);
        Tplayer = player.transform;
        dotPortals = Vector3.Dot(Tplayer.transform.forward, tpPoint.transform.forward);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Player")
        {
            //musicManager.levelFinished(frameManager.addCompleteLevel());

            Vector3 portalToPlayer = Tplayer.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
            if (dotProduct < 0.0f)
            {
                Camera mainCamera = Camera.main;

                player = col.transform.gameObject;

                float myF = player.transform.eulerAngles.y + (tpPoint.transform.eulerAngles.y - transform.eulerAngles.y + 180.0f);

                float cameraOffset = player.transform.position.y - mainCamera.transform.position.y;

                if (cameraB != null)
                {
                    player.transform.position = new Vector3(cameraB.position.x, cameraB.position.y + cameraOffset, cameraB.position.z);
                }
                else
                {
                    player.transform.position = tpPoint.transform.position + player.transform.position - transform.position;
                }

                mainCamera.GetComponent<CameraMovement>().SetMouseLookX(myF);
            }
        }
        
    }
}
