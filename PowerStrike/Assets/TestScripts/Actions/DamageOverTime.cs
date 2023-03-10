using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageOverTime : Action
{
    private int damagePerTick; //over-time damage
    private float damageInterval; //time between ticks
    private int ticks; //number of ticks
    
    public void ScheduleDamage(int dmg, int ts, float val, GameObject targ)
    {
        damagePerTick = dmg;
        ticks = ts;
        damageInterval = val;
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
            //do the damage
            Health hp = target.GetComponent<Health>();
            hp.ChangeHealth(-damagePerTick);

            //decrement the tick
            ticks--;

            //@@@ debugging: show bleed in the Status Effect box
            TextMeshProUGUI text = GameObject.Find("StatusEffects").GetComponent<TextMeshProUGUI>();

            //text.text = string.Format("DoT's: {0}", queue.ReturnActionType(this.GetType(), target));
            

            //call schedule damage again - pass self parameters
            ScheduleDamage(damagePerTick, ticks, damageInterval, target);
        }
        else
        {
            //destroy the script
            Destroy(this);
        }
    }
}
