using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //the following copy/paste from the player script... maybe should've made a "turn manager"
    public float turnInterval; //increment of turn timer
    public Text temp; //just to "simulate" a turn for the opponent for now
    private bool waiting; //used to ensure we don't call certain coroutines multiple times over
    public Timer clock; //just to track global game timer!
    private TurnManager turnManager; //to assign the turn manager for this entity

    //damage and healing vars
    private Damage basicAttack; //basic hit
    private DamageOverTime dot; //damage over time
    private Heal heal; //basic heal
    private HealOverTime hot; //heal over time
    
    //public Player player; //simply to check for turn boolean (old)

    // Start is called before the first frame update
    void Start()
    {
        turnManager = this.GetComponent<TurnManager>();
        turnManager.SetTurnInt(turnInterval);
        turnManager.SetNextTurn(turnInterval);
        temp.gameObject.SetActive(false);
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

    //simulates the NPC turn
    private void EndTurn()
    {
        waiting = false;
        turnManager.SetNextTurn(turnInterval);
        turnManager.SetTurn(false);
        clock.ContGame();
        temp.gameObject.SetActive(false);
    }

    private IEnumerator DoTurn(float time)
    {
        temp.gameObject.SetActive(true);
        temp.text = "Thinking...";
        yield return new WaitForSeconds(time);
        
        Health hp = this.GetComponent<Health>();
        int decision;

        if (hp.GetCurrentHealth() >= hp.GetMaxHealth())
            decision = 0; //Random.Range(0, 2)
        else
            decision = 0; //@@@ need to change back to 4 

        switch (decision)
        {
            case 0:
                //damage one time
                Attack(5, 0);

                temp.text = "Decision made: attack";
                break;
            case 1:
                //damage over time
                temp.text = "Decision made: bleed";
                DamageOverTime(3, 10, 0.75f);
                break;
            case 2:
                //heal once
                Heal(10, 0);
                temp.text = "Decision made: heal";
                break;
            case 3:
                //heal over time
                HealOverTime(2, 5, 0.5f);
                temp.text = "Decision made: heal over time";
                break;
        }
        yield return new WaitForSeconds(time);
        EndTurn();
    }

    private void Attack(int dmg, float interval)
    {
        basicAttack = ScriptableObject.CreateInstance<Damage>();
        basicAttack.ScheduleDamage(dmg, interval, GameObject.Find("Player"));
        //the below commented out code block WORKS in stunning the target.
        /*GameObject player = GameObject.Find("Player");
        Player pl = player.GetComponent<Player>();
        pl.StunMe(1.0f);*/
    }

    private void DamageOverTime(int dmg, int ts, float interval)
    {
        dot = ScriptableObject.CreateInstance<DamageOverTime>();
        dot.ScheduleDamage(dmg, ts, interval, GameObject.Find("Player"));
    }

    private void Heal(int amt, float interval)
    {
        heal = ScriptableObject.CreateInstance<Heal>();
        heal.ScheduleHeal(amt, interval, this.gameObject);
    }

    private void HealOverTime(int amt, int ts, float interval)
    {
        hot = ScriptableObject.CreateInstance<HealOverTime>();
        hot.ScheduleHeal(amt, ts, interval, this.gameObject);
    }
}
