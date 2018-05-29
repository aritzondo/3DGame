using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBlueBridgeController : MonoBehaviour
{
    public ActivatorGeneric activator;
    public GameObject blueFloor;

    private bool blueUp = true;
    private RotateBridge bridgeRot;

    void Start()
    {
        bridgeRot = GetComponent<RotateBridge>();
    }

    // Update is called once per frame
    void Update()
    {
        bridgeRot.Trigger = activator.Active; //Input.GetButtonDown("Fire1");

        blueFloor.SetActive(bridgeRot.CurrentRot > 1);
        activator.Active = false;
    }
}