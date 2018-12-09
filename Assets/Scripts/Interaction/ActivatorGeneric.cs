using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorGeneric : Activable
{
    private Animator anim;
    private DoorMovement thisDoor;
    private int ilumHash;
    private int notIlumHash;
    private bool active = false;

    public bool Active
    {
        get { return active; }
        set { active = value; }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        ilumHash = Animator.StringToHash("Iluminated");
        notIlumHash = Animator.StringToHash("NotIlum");
    }

    public override void enterInLight()
    {
        anim.SetTrigger(ilumHash);
        base.enterInLight();
        active = true;
    }

    public override void exitLight()
    {
        anim.SetTrigger(notIlumHash);
        base.exitLight();
        active = false;
    }
}
