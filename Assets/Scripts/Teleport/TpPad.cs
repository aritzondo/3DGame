using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPad : MonoBehaviour {

    public Transform Tplayer;
    public GameObject tpPoint;

    private GameObject player;
    private float dotPortals;

    private void Start()
    {
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
                player = col.transform.gameObject;

                float myF = player.transform.eulerAngles.y + (tpPoint.transform.eulerAngles.y - transform.eulerAngles.y + 180.0f);
                
                Vector3 newPos = player.transform.position;
                newPos.y = tpPoint.transform.position.y + (newPos.y - transform.position.y);

                if (dotPortals < 0)
                {
                    newPos.x = tpPoint.transform.position.x - (newPos.x - transform.position.x);
                    newPos.z = tpPoint.transform.position.z - (newPos.z - transform.position.z);
                }
                else
                {
                    newPos.x = tpPoint.transform.position.x + (newPos.x - transform.position.x);
                    newPos.z = tpPoint.transform.position.z + (newPos.z - transform.position.z);
                }
                player.transform.position = newPos;

                Camera.main.GetComponent<CameraMovement>().SetMouseLookX(myF);
            }
        }
        
    }
}
