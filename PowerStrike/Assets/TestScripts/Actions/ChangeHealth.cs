using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealth : Action
{
    //variables pulled from parent: target, time, clock, and queue
    //functions: AssignTarget (with and without time), GetTarget, GetTime, DoAction (must be created here)
    //USE:
    //  one time change in health
    //  value to change will be passed as positive or negative (heal or damage)
    //      this means you do not need to adjust the sign of the value in this script

    //new variables for this child class
    private int change; //the value to alter your hp

    //schedule the change to occur:
    public void ScheduleChange(int chg, float val, GameObject targ, GameObject own)
    {
        change = chg; //assign the value
        target = targ; //assign the target
        owner = own; //assign the owner of this ability

        if (val == 0)
        {
            time = clock.GetTime() + 0.02f; //this is an "instant" ability
        }
        else
        {
            time = clock.GetTime() + val; //delayed ability
        }

        queue.AddAction(this); //queue it up to occur
    }

    //when your turn comes up: do this
    public override void DoAction()
    {
        //insert logic check for owner being alive (ability shouldn't trigger)
        if(owner != null)
        {
            //insert logic to change health
            time = 0; //@@@ need to investigate this, why is this even here?
            Health hp = target.GetComponent<Health>();
            hp.ChangeHealth(change);
        }

        //insert logic to destroy this once complete (first remove from queue)
        //queue.RemoveAction(this);
        Destroy(this);
    }
}
