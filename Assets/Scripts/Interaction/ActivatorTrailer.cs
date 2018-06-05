using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorTrailer : Activable
{
    public Light lightToTurnOn;

    private Animator anim;
    private int ilumHash;
    private int notIlumHash;
    private bool active = false;

    public bool Active
    {
        get { return active; }
        set { active = value; }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        ilumHash = Animator.StringToHash("Iluminated");
        notIlumHash = Animator.StringToHash("NotIlum");
    }

    public override void enterInLight()
    {
        anim.SetTrigger(ilumHash);
        base.enterInLight();
        lightToTurnOn.enabled = true;
        active = true;
    }

    public override void exitLight()
    {
        anim.SetTrigger(notIlumHash);
        base.exitLight();
        active = false;
    }
}
