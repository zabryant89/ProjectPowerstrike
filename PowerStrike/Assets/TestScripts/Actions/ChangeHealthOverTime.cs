using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthOverTime : Action
{
    //variables pulled from parent: target, time, clock, and queue
    //functions: AssignTarget (with and without time), GetTarget, GetTime, DoAction (must be created here)
    //USE:
    //  change in health over time, the TOTAL damage dealt needs to be passed along with the number of ticks AND the interval between ticks. Remainder damage will be dealt on one last tick beyond the last.
    //  value to change will be passed as positive or negative (heal or damage)
    //      this means you do not need to adjust the sign of the value in this script

    //variables for this class:
    private int changePerTick; //over-time heal/damage (pos/neg)
    private float changeInterval; //time between each tick
    private int remainder; //remainder of the change, applied at the very end
    private int ticks; //number of ticks

    //overloaded method
    //this one is the initial creation of the change
    public void ScheduleChange(int val, int ts, float paramTime, GameObject targ, GameObject own)
    {
        remainder = val % ts;
        changePerTick = val / ts;
        ticks = ts;
        changeInterval = paramTime;
        target = targ;
        owner = own;

        if (val == 0)
        {
            time = clock.GetTime();
            queue.AddAction(this);
        }
        else
        {
            time = clock.GetTime() + paramTime;
            queue.AddAction(this);
        }
    }

    //overloaded method
    //this is the one that continues the chain of change
    public void ScheduleChange(int val, int ts, float paramTime, GameObject targ, GameObject own, int rem)
    {
        remainder = rem;
        changePerTick = val;
        ticks = ts;
        changeInterval = paramTime;
        target = targ;
        owner = own;

        if (val == 0)
        {
            time = clock.GetTime();
            queue.AddAction(this);
        }
        else
        {
            time = clock.GetTime() + paramTime;
            queue.AddAction(this);
        }
    }

    public override void DoAction()
    {
        //remaining ticks to trigger?
        //NO: final override of over time: remainder is added 1 by one
        if (ticks != 0)
        {
            //do the change
            Health hp = target.GetComponent<Health>();

            //do the changes this tick
            if (remainder == 0)
            {
                //no remainder
                hp.ChangeHealth(changePerTick);
            }
            else if (remainder < 1)
            {
                //damage
                hp.ChangeHealth(changePerTick - 1);
                remainder++;
            }
            else
            {
                //heal
                hp.ChangeHealth(changePerTick + 1);
                remainder--;
            }

            //decrement the ticks
            ticks--;

            //call the next tick of damage
            ScheduleChange(changePerTick, ticks, changeInterval, target, owner, remainder);
        }
        else
        {
            //calculate remaining amount, if any, and apply it
            //this still occurs as there may be a rare case where remainder is greater than 1 after all the ticks
            if (remainder != 0)
            {
                Health hp = target.GetComponent<Health>();
                hp.ChangeHealth(remainder);
            }

            //remove from action queue and destroy the script if it's the last one
            //queue.RemoveAction(this);
            Destroy(this);
        }
    }
}
