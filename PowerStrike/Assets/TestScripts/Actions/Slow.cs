using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Action
{
    //how to handle slows?
    // simple: each character will have a "SetSlow" function that sets a boolean AND a timer
    //          timer counts down, when <= 0 it turns slow to false
    //So why this script?
    //trigger that function on the target and determine the slow time

    float slowTime;

    public void ScheduleSlow(float val, GameObject targ)
    {
        slowTime = val;
        target = targ;

        time = clock.GetTime() + 0.02f;
        queue.AddAction(this);
    }

    public override void DoAction()
    {
        //need to access the method SetSlow() from here
        Character targ = target.GetComponent<Character>();

        targ.SetSlow(slowTime, true);
    }
}
