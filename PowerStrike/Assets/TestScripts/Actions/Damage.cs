using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Action
{
    //one time damage
    private int damage; //single time damage

    public void ScheduleDamage(int dmg, float val, GameObject targ)
    {
        damage = dmg;
        target = targ;

        if (val == 0)
        {
            time = clock.GetTime() + 0.02f; //this MUST occur FOR NOW as to not have the attack occur on selection if simultaneous turns occur
            queue.AddAction(this);
        }
        else
        {
            time = clock.GetTime() + val;
            queue.AddAction(this);
        }
    }

    public override void DoAction()
    {
        //insert damage logic here!
        time = 0; 

        Health hp = target.GetComponent<Health>();
        hp.ChangeHealth(-damage);

        //need to destroy the item now that it's function is complete!
        Destroy(this);
    }
}
