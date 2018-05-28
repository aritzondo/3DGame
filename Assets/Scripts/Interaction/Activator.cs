using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : Activable
{

    private Animator anim;
    private int ilumHash;
    private int notIlumHash;

    void Start()
    {
        anim = GetComponent<Animator>();
        ilumHash = Animator.StringToHash("Iluminated");
        notIlumHash = Animator.StringToHash("NotIlum");
    }

    public override void enterInLight()
    {
        if (timeInLight == 0)
        {
            anim.SetTrigger(ilumHash);
        }
        base.enterInLight();        
    }

    public override void exitLight()
    {
        base.exitLight();
        anim.SetTrigger(notIlumHash);
    }

    public override void Activate()
    {
        //call the function to do the thing
    }
}
