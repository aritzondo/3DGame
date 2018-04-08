using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRoom : MonoBehaviour {

    public GameObject orangeFloor1;
    public GameObject orangeFloor2;
    public GameObject orangeFloor3;

    private bool orangeUp = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (orangeUp)
        {
            orangeFloor1.SetActive(false);
            orangeFloor2.SetActive(false);
            orangeFloor3.SetActive(false);
        }
        else
        {
            orangeFloor1.SetActive(true);
            orangeFloor2.SetActive(true);
            orangeFloor3.SetActive(true);
        }
    }
}
