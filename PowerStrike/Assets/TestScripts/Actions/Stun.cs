using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Action
{
    private float stunTime;

    public void ScheduleStun(float val, GameObject targ)
    {
        stunTime = val;
        AssignTarget(targ);

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
        if (target.name == "Player")
        {
            Player targ = target.GetComponent<Player>();
            targ.StunMe(stunTime);
        }
        else
        {
            Enemy targ = target.GetComponent<Enemy>();
            targ.StunMe(stunTime);
        }
    }
}
