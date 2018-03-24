using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPad : MonoBehaviour {

    private GameObject player;
    public Transform Tplayer;
    public GameObject tpPoint;

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

                player.transform.position = tpPoint.transform.position + (player.transform.position - transform.position);

                Camera.main.GetComponent<CameraMovement>().SetMouseLookX(myF);
            }
        }
        
    }
}
