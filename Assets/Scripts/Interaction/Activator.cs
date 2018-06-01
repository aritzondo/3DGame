using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : Activable
{
    public GameObject door;

    private Animator anim;
    private DoorMovement thisDoor;
    private int ilumHash;
    private int notIlumHash;

    void Start()
    {
        thisDoor = door.GetComponent<DoorMovement>();
        anim = GetComponent<Animator>();
        ilumHash = Animator.StringToHash("Iluminated");
        notIlumHash = Animator.StringToHash("NotIlum");
    }

    public override void enterInLight()
    {
        if(!inLight)
            anim.SetTrigger(ilumHash);
        base.enterInLight();        
    }

    public override void exitLight()
    {
        base.exitLight();
        if (!activated)
        {
            anim.SetTrigger(notIlumHash);
            thisDoor.startClosing();
        }
    }

    public override void Activate()
    {
       thisDoor.startMovement();
    }
}
