using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBlueRoomController : MonoBehaviour {

    public ActivatorGeneric activator;
    public GameObject orangeFloor1;
    public GameObject orangeFloor2;
    public GameObject orangeFloor3;
    public GameObject leftWall;
    public GameObject rightWall;
    public Material orange;
    public RotateBridge firstRoom;

    private Material blue;
    private Renderer lRenderer;
    private Renderer rRenderer;

    private bool orangeUp = true;
    private RotateBridge bridgeRot;

    // Use this for initialization
    void Start()
    {
        bridgeRot = GetComponent<RotateBridge>();
        lRenderer = leftWall.GetComponent<Renderer>();
        rRenderer = rightWall.GetComponent<Renderer>();
        blue = lRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        bridgeRot.Trigger = activator.Active; //= Input.GetButtonDown("Fire2");
        firstRoom.Trigger = bridgeRot.Trigger;
        bool orangeOn = bridgeRot.CurrentRot < Mathf.Epsilon;

        orangeFloor1.SetActive(orangeOn);
        orangeFloor2.SetActive(orangeOn);
        orangeFloor3.SetActive(orangeOn);
        switch (orangeOn)
        {
            case false:
                lRenderer.material = orange;
                rRenderer.material = orange;
                break;
            case true:
                lRenderer.material = blue;
                rRenderer.material = blue;
                break;
        }
        activator.Active = false;
    }
}
