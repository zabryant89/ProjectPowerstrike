using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    //public Player player; //simply to check for turn boolean (old)

    //enemy specific stuff, partly for debugging
    public Text temp; //just to "simulate" a turn for the opponent for now
    private bool waiting; //used to ensure we don't call certain coroutines multiple times over

    //debug variables
    private bool skip = false; //skip all actions

    //stats
    private int power; //damage modifier

    // Start is called before the first frame update
    void Start()
    {
        //@@@ debug: turn below on if you need to disable AI actions
        skip = true;
        basAtkInt = 1.8f;

        turnManager = this.GetComponent<TurnManager>();
        turnManager.SetBasicAttack(basAtkInt);
        turnManager.SetEntity(this);
        turnManager.SetTurnInt(turnInterval);
        turnManager.SetNextTurn(turnInterval);
        temp.gameObject.SetActive(false);

        power = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnManager.GetTurn() && !waiting)
        {
            waiting = true;
            StartCoroutine(DoTurn(4f));
        }
    }

    override protected int CalcDamage(int baseline, bool overtime)
    {
        int final;
        if (overtime)
            final = baseline + power / 4; //overtime effects will need less impact
        else
            final = baseline + power; //normal attacks

        return final;
    }

    override protected float CalcSpeed(float modifier, float abilitySpeed)
    {
        float final;
        final = abilitySpeed * modifier; //use ability speed instead

        return final;
    }

    /*override public void BasicAttack()
    {
        if (skip)
        {
            turnManager.SetBasicAttack(basAtkInt);
            return;
        }

        changeHP = ScriptableObject.CreateInstance<ChangeHealth>();
        changeHP.ScheduleChange(-CalcDamage(power, false, true), 0, GameObject.Find("Player"), this.gameObject);
        turnManager.SetBasicAttack(basAtkInt);
    }*/

    //simulates the NPC turn
    override protected void EndTurn(float next)
    {
        waiting = false;
        turnManager.SetNextTurn(next);
        turnManager.SetTurn(false);
        clock.ContGame();
        temp.gameObject.SetActive(false);
    }

    private IEnumerator DoTurn(float time)
    {
        temp.gameObject.SetActive(true);
        temp.text = "Thinking...";
        yield return new WaitForSeconds(time);

        /*//debugging basic attacks for now:
        EndTurn(turnInterval);*/
        
        //crappy "AI" implementation
        Health hp = this.GetComponent<Health>();
        int decision;

        if (hp.GetCurrentHealth() >= hp.GetMaxHealth())
            decision = Random.Range(0, 2);
        else if (hp.GetCurrentHealth() > (int)(hp.GetMaxHealth() * 0.3))
            decision = Random.Range(0, 4); //above 30% health, all options 
        else
            decision = Random.Range(2, 4); //lower than 30%, heal only

        if (skip)
        {
            EndTurn(2.5f);
            yield break;
        }

        switch (decision)
        {
            case 0:
                //damage one time
                temp.text = "Decision made: attack";
                yield return new WaitForSeconds(time);
                Attack(10, 0, CalcSpeed(1f, turnInterval));
                break;
            case 1:
                //damage over time
                temp.text = "Decision made: bleed";
                yield return new WaitForSeconds(time);
                DamageOverTime(CalcDamage(52, true), 10, 0.75f, CalcSpeed(0.5f, turnInterval));
                break;
            case 2:
                //heal once
                temp.text = "Decision made: heal";
                yield return new WaitForSeconds(time);
                Heal(15, 0, CalcSpeed(1f, turnInterval));
                break;
            case 3:
                //heal over time
                temp.text = "Decision made: heal over time";
                yield return new WaitForSeconds(time);
                HealOverTime(28, 5, 0.5f, CalcSpeed(0.5f, turnInterval));
                break;
        }
    }

    override protected void Attack(int dmg, float interval, float nextTurn)
    {
        changeHP = ScriptableObject.CreateInstance<ChangeHealth>();
        changeHP.ScheduleChange(-dmg, interval, GameObject.Find("Player"), this.gameObject);
        //the below commented out code block WORKS in stunning the target.
        /*GameObject player = GameObject.Find("Player");
        Player pl = player.GetComponent<Player>();
        pl.StunMe(1.0f);*/
        EndTurn(nextTurn);
    }

    override protected void DamageOverTime(int dmg, int ts, float interval, float nextTurn)
    {
        changeHPOT = ScriptableObject.CreateInstance<ChangeHealthOverTime>();
        changeHPOT.ScheduleChange(-dmg, ts, interval, GameObject.Find("Player"), this.gameObject);
        /*dot = ScriptableObject.CreateInstance<DamageOverTime>();
        dot.ScheduleDamage(dmg, ts, interval, GameObject.Find("Player"));*/
        EndTurn(nextTurn);
    }

    override protected void Heal(int amt, float interval, float nextTurn)
    {
        changeHP = ScriptableObject.CreateInstance<ChangeHealth>();
        changeHP.ScheduleChange(amt, interval, this.gameObject, this.gameObject);
        /*heal = ScriptableObject.CreateInstance<Heal>();
        heal.ScheduleHeal(amt, interval, this.gameObject);*/
        EndTurn(nextTurn);
    }

    override protected void HealOverTime(int amt, int ts, float interval, float nextTurn)
    {
        changeHPOT = ScriptableObject.CreateInstance<ChangeHealthOverTime>();
        changeHPOT.ScheduleChange(amt, ts, interval, this.gameObject, this.gameObject);
        /*hot = ScriptableObject.CreateInstance<HealOverTime>();
        hot.ScheduleHeal(amt, ts, interval, this.gameObject);*/
        EndTurn(nextTurn);
    }

    public override void StunMe(float stunTime)
    {
        turnManager.SetNextTurn(stunTime);
        turnManager.SetBasicAttack(stunTime);
    }

    protected override void StunTarget(float stunTime)
    {
        throw new System.NotImplementedException();
    }
}
