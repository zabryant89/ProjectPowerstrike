using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Action
{
    private int heal; //amount to be healed at one time

    public void ScheduleHeal(int amt, float val, GameObject targ)
    {
        heal = amt;
        target = targ;

        if (val == 0)
        {
            time = clock.GetTime() + 0.02f;
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
        //insert heal logic here!
        time = 0;

        Health hp = target.GetComponent<Health>();
        hp.ChangeHealth(heal);

        //need to destroy the item now that it's function is complete!
        Destroy(this);
    }
}
