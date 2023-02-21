using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Character : MonoBehaviour
{
    //need: player turn timer, pause the timer when turn is up, continue timer after player action.
    //          Timer object to keep an eye on time!
    public float turnInterval; //increment of turn timer (baseline)
    public Timer clock; //just to track global game timer!
    protected TurnManager turnManager; //local turn manager script just for this object

    //action variables
    protected Damage attack; //basic attack
    protected float basAtkInt; //basic attack interval - will be determined by weapon, but here for now!
    protected DamageOverTime dot; //bleed attack
    protected Heal heal; //healing
    protected HealOverTime hot; //heal over time

    /* pass the base damage of the action and a bool if it is over time or not*/
    abstract protected int CalcDamage(int baseline, bool overtime, bool basic);

    abstract protected float CalcSpeed(float modifier, float abilitySpeed);

    abstract public void BasicAttack();

    abstract protected void Attack(int dmg, float interval, float nextTurn);

    abstract protected void DamageOverTime(int dmgPer, int ts, float interval, float nextTurn);

    abstract protected void Heal(int amt, float interval, float nextTurn);

    abstract protected void HealOverTime(int amt, int ts, float interval, float nextTurn);

    abstract public void StunMe(float stunTime);

    abstract protected void EndTurn(float next);
}
