using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorTest : Activable {

    public Material ilumColor;

	public override void Activate()
    {
        GetComponent<Renderer>().material = ilumColor;
    }
}
