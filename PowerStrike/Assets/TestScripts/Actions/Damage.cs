using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Action
{
    //one time damage
    private int damage; //single time damage

    //over time damage - SCRATCH THIS! - will do a separate script for this
    private int damagePerTick; //over-time damage
    private float damageInterval; //time between ticks
    private int ticks; //number of ticks


    public void ScheduleDamage(int dmg, float val, GameObject targ)
    {
        time = val;
        damage = dmg;
        target = targ;
    }

    public override void DoAction()
    {
        //insert damage logic here!
    }
}
