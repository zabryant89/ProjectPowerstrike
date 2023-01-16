using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    //health script
    private Health health;

    //timer
    public Timer clock;

    //globals
    private List<int> burnTicks; //tracks the remaining ticks of each item
    private float bleedTime; //bleed time (will run in separate coroutines)
    private int bleedDamage; //bleed damage per tick
    private int bleedTicks;
    

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    //start with burns (pass the number of ticks)
    //burn rules:
    //      damage doesn't stack
    //      ticks "overlap" meaning only the highest count matters
    //      burns every second
    //      damage = always 30 or 2% of health (whichever is lower!) (to prevent insane boss damage)
    //      damage occurs upon application as well as during each tick (first hit still counts as a tick!)
    public void ApplyBurn(int tick)
    {
        if (burnTicks.Count <= 0)
        {
            burnTicks.Add(tick);
            StartCoroutine(Burn(clock.GetTime() + 1.0f));
        }
        else
        {
            burnTicks.Add(tick);
        }
    }

    private IEnumerator Burn(float startTime)
    {
        while (burnTicks.Count > 0 && clock.GetTime() >= startTime)
        {
            //decrement ticks
            for (int i = 0; i < burnTicks.Count; i++)
                burnTicks[i]--;
            //deal damage
            if ((health.GetMaxHealth()*0.02) < 30)
            {
                int damage = -(int)Math.Floor(health.GetMaxHealth() * 0.02);
                health.ChangeHealth(damage);
            }
            else
                health.ChangeHealth(-30);
            //remove 0 ticks from list
            burnTicks.RemoveAll(num => num == 0);

            //wait until next tick time.
            startTime++;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
