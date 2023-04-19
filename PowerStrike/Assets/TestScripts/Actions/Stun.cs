using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Action
{
    private float stunTime;

    public void ScheduleStun(float val, GameObject targ)
    {
        Debug.Log("Assigning stun values");
        stunTime = val;
        target = targ;

        Debug.Log("Values assigned");

        //I found my problem... VAL
        time = clock.GetTime() + 0.02f;
        queue.AddAction(this);

        Debug.Log("Stun Scheduled!");
    }

    public override void DoAction()
    {
        Debug.Log("Determining target for stun");

        Character targ = target.GetComponent<Character>();

        targ.StunMe(stunTime);

        /*if (target.name == "Player")
        {
            Debug.Log("Player chosen for stun");
            Player targ = target.GetComponent<Player>();
            targ.StunMe(stunTime);
        }
        else
        {
            Debug.Log("Enemy chosen for stun");
            Enemy targ = target.GetComponent<Enemy>();
            targ.StunMe(stunTime);
        }*/
    }
}
