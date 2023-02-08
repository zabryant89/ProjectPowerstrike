using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : Action
{
    private int healPerTick; //over-time damage
    private float healInterval; //time between ticks
    private int ticks; //number of ticks

    public void ScheduleHeal(int amt, int ts, float val, GameObject targ)
    {
        healPerTick = amt;
        ticks = ts;
        healInterval = val;
        target = targ;

        if (val == 0)
        {
            time = clock.GetTime();
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
        if (ticks != 0)
        {
            //do the heal
            Health hp = target.GetComponent<Health>();
            hp.ChangeHealth(healPerTick);

            //decrement the tick
            ticks--;

            //call schedule heal again - pass self parameters
            ScheduleHeal(healPerTick, ticks, healInterval, target);
        }
        else
        {
            //destroy the script
            Destroy(this);
        }
    }
}
