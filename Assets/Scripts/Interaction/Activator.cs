using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : Activable
{

    private Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    public override void Activate()
    {
        anim.SetBool("Active",true);
    }
}
