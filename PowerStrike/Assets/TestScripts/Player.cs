using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //need: player turn timer, pause the timer when turn is up, continue timer after player action.
    //          Timer object to keep an eye on time!
    public float turnInterval; //increment of turn timer (baseline)
    public Timer clock; //just to track global game timer!
    private TurnManager turnManager; //local turn manager script just for this object

    //action variables
    private Damage attack; //basic attack
    private DamageOverTime bleedAttack; //bleed attack
    private Heal heal; //healing
    private HealOverTime hot; //heal over time

    //stats - affects damage/healing/etcs
    private int power; //damage modifier


    // Start is called before the first frame update
    void Start()
    {
        turnManager = this.GetComponent<TurnManager>();
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
                Attack(CalcDamage(0, false), 0, CalcSpeed(1, turnInterval));
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                DamageOverTime(CalcDamage(6, true), 5, 0.75f, CalcSpeed(1, 1f));
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                Heal(15, 0, turnInterval);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                HealOverTime(5, 5, 0.5f, CalcSpeed(1, 1f));
            }
        }
    }

    /* pass the base damage of the action and a bool if it is over time or not*/
    private int CalcDamage(int baseline, bool overtime)
    {
        int final;
        if (overtime)
            final = baseline + power / 4; //over time effects will need less impact
        else
            final = baseline + power; //normal attacks
        
        return final;
    }

    private float CalcSpeed(float modifier, float abilitySpeed)
    {
        float final;
        final = abilitySpeed * modifier; //use the ability's speed instead

        return final;
    }

    private void Attack(int dmg, float interval, float nextTurn)
    {
        attack = ScriptableObject.CreateInstance<Damage>();
        //basicAttack.AssignTarget(GameObject.Find("Enemy")); //derp... unecessary
        attack.ScheduleDamage(dmg, interval, GameObject.Find("Enemy"));
        //Destroy(basicAttack); //DO NOT DO THIS HERE, ruins the entry action queue
        EndTurn(nextTurn);
    }

    private void DamageOverTime(int dmgPer, int ts, float interval, float nextTurn)
    {
        bleedAttack = ScriptableObject.CreateInstance<DamageOverTime>();
        bleedAttack.ScheduleDamage(dmgPer, ts, interval, GameObject.Find("Enemy"));
        EndTurn(nextTurn);
    }

    private void Heal(int amt, float interval, float nextTurn)
    {
        heal = ScriptableObject.CreateInstance<Heal>();
        heal.ScheduleHeal(amt, interval, this.gameObject);
        EndTurn(nextTurn);
    }

    private void HealOverTime(int amt, int ts, float interval, float nextTurn)
    {
        hot = ScriptableObject.CreateInstance<HealOverTime>();
        hot.ScheduleHeal(amt, ts, interval, this.gameObject);
        EndTurn(nextTurn);
    }

    private void EndTurn(float next)
    {
        turnManager.SetNextTurn(next);
        turnManager.SetTurn(false);
        clock.ContGame();
    }
}
