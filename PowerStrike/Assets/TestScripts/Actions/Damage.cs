using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Action
{
    //one time damage
    private int damage; //single time damage
    private bool ready = true; //false = waiting to go, true = fired off!

    //over time damage - SCRATCH THIS! - will do a separate script for this
    private int damagePerTick; //over-time damage
    private float damageInterval; //time between ticks
    private int ticks; //number of ticks

    public void ScheduleDamage(int dmg, float val, GameObject targ)
    {
        damage = dmg;
        target = targ;

        if (val == 0)
        {
            ready = false;
            time = clock.GetTime() + 0.02f; //this MUST occur FOR NOW as to not have the attack occur right now if simultaneous turns occur
            queue.AddAction(this);
        }
        else
        {
            ready = false;
            time = clock.GetTime() + val;
            queue.AddAction(this);
        }
    }

    public override void DoAction()
    {
        //insert damage logic here!
        time = 0;
        ready = true;

        Health hp = target.GetComponent<Health>();
        hp.ChangeHealth(-damage);

        //need to destroy the item now that it's function is complete!
        Destroy(this);
    }
}
