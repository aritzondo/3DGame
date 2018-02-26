using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPad : MonoBehaviour {

    private GameObject player;
    public GameObject tpPoint;

    private void OnTriggerEnter(Collider col)
    {
        

        if (col.tag=="Player")
        {
            player = col.transform.gameObject;

            float myF = player.transform.eulerAngles.y + (tpPoint.transform.eulerAngles.y - transform.eulerAngles.y)+180.0f;

            Camera.main.GetComponent<CameraMovement>().SetMouseLookX(myF);
            player.transform.position = tpPoint.transform.position + (player.transform.position - transform.position);
        }
    }
}
