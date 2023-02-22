using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    //stats - affects damage/healing/etcs
    private int power; //damage modifier
    private Enemy target; // enemy target? might need I think


    // Start is called before the first frame update
    void Start()
    {
        basAtkInt = 2.2f;

        turnManager = this.GetComponent<TurnManager>();
        turnManager.SetBasicAttack(basAtkInt);
        turnManager.SetEntity(this);
        turnManager.SetTurnInt(turnInterval);
        turnManager.SetNextTurn(turnInterval);

        power = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (turnManager.GetTurn())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndTurn(turnInterval);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Attack(10, 0, CalcSpeed(1, turnInterval));
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                DamageOverTime(6, 5, 0.75f, CalcSpeed(0.5f, turnInterval));
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                Heal(15, 0, turnInterval);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                HealOverTime(5, 5, 0.5f, CalcSpeed(0.5f, turnInterval));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StunTarget(1f); //why no work in regular game but work in debug?
            }
        }
    }

    /* pass the base damage of the action and a bool if it is over time or not*/
    override protected int CalcDamage(int baseline, bool overtime, bool basic)
    {
        int final;
        if (overtime)
            final = baseline + power / 4; //over time effects will need less impact
        else if (!basic)
            final = baseline + power; //normal attacks
        else
            final = baseline + (int)(power * (basAtkInt / 5)); //basic attack
        
        return final;
    }

    override protected float CalcSpeed(float modifier, float abilitySpeed)
    {
        float final;
        final = abilitySpeed * modifier; //use the ability's speed instead

        return final;
    }

    override public void BasicAttack()
    {
        attack = ScriptableObject.CreateInstance<Damage>();
        attack.ScheduleDamage(CalcDamage(power, false, true), 0, GameObject.Find("Enemy"));
        turnManager.SetBasicAttack(basAtkInt);
    }

    override protected void Attack(int dmg, float interval, float nextTurn)
    {

        attack = ScriptableObject.CreateInstance<Damage>();
        attack.ScheduleDamage(dmg, interval, GameObject.Find("Enemy"));
        EndTurn(nextTurn);
    }

    override protected void DamageOverTime(int dmgPer, int ts, float interval, float nextTurn)
    {
        dot = ScriptableObject.CreateInstance<DamageOverTime>();
        dot.ScheduleDamage(dmgPer, ts, interval, GameObject.Find("Enemy"));
        EndTurn(nextTurn);
    }

    override protected void Heal(int amt, float interval, float nextTurn)
    {
        heal = ScriptableObject.CreateInstance<Heal>();
        heal.ScheduleHeal(amt, interval, this.gameObject);
        EndTurn(nextTurn);
    }

    override protected void HealOverTime(int amt, int ts, float interval, float nextTurn)
    {
        hot = ScriptableObject.CreateInstance<HealOverTime>();
        hot.ScheduleHeal(amt, ts, interval, this.gameObject);
        EndTurn(nextTurn);
    }

    override public void StunMe(float stunTime)
    {
        turnManager.SetNextTurn(stunTime);
        turnManager.SetBasicAttack(stunTime);
    }

    override protected void StunTarget(float stunTime)
    {
        GameObject targ = GameObject.Find("Enemy");
        stun.ScheduleStun(stunTime, targ);
    }

    override protected void EndTurn(float next)
    {
        turnManager.SetNextTurn(next);
        turnManager.SetTurn(false);
        clock.ContGame();
    }
}
