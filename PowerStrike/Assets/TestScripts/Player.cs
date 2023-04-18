using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : Character
{
    //stats - affects damage/healing/etcs
    private int power; //damage modifier
    private Enemy target; // enemy target? might need I think

    //spell variables
    private bool casting; //tells us if you're casting or not, if you are... delays/interrupts
    private float castTime; //this will change based on the spell, but for now a single variable should cover it

    //control
    private GameObject menu; //control menu
    private Button[] buttons; //buttons for the menu (max 6)


    // Start is called before the first frame update
    void Start()
    {
        basAtkInt = 2.2f;

        turnManager = this.GetComponent<TurnManager>();
        turnManager.SetBasicAttack(basAtkInt);
        turnManager.SetEntity(this);
        turnManager.SetTurnInt(turnInterval);
        turnManager.SetNextTurn(turnInterval, false);

        power = 10;

        menu = GameObject.Find("ButtonControls");
        buttons = menu.GetComponentsInChildren<Button>();

        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = "Offense";
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Defense";

        for(int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnClickButton(buttonIndex));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (casting && clock.GetTime() >= castTime)
        {

        }
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
                DamageOverTime(31, 5, 0.75f, CalcSpeed(0.5f, turnInterval));
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                Heal(15, 0, turnInterval);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                HealOverTime(28, 5, 0.5f, CalcSpeed(0.5f, turnInterval));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                StunTarget(1f, 2f); //why no work in regular game but work in debug?
            }
        }
    }

    void OnClickButton(int index)
    {
        switch (index)
        {
            case 0:
                if (turnManager.GetTurn())
                {
                    Attack(10, 0, CalcSpeed(1, turnInterval));
                }
                break;
            case 1:
                if (turnManager.GetTurn())
                {
                    Heal(15, 0, turnInterval);
                }
                break;
        }       
    }

    /* pass the base damage of the action and a bool if it is over time or not*/
    override protected int CalcDamage(int baseline, bool overtime)
    {
        int final;
        if (overtime)
            final = baseline + power / 4; //over time effects will need less impact
        else
            final = baseline + power; //normal attacks
        
        return final;
    }

    override protected float CalcSpeed(float modifier, float abilitySpeed)
    {
        float final;
        final = abilitySpeed * modifier; //use the ability's speed instead

        return final;
    }

    override protected void Attack(int dmg, float interval, float nextTurn)
    {

        changeHP = ScriptableObject.CreateInstance<ChangeHealth>();
        changeHP.ScheduleChange(-dmg, interval, GameObject.Find("Enemy"), this.gameObject);
        EndTurn(nextTurn);
    }

    override protected void DamageOverTime(int dmgPer, int ts, float interval, float nextTurn)
    {
        changeHPOT = ScriptableObject.CreateInstance<ChangeHealthOverTime>();
        changeHPOT.ScheduleChange(-dmgPer, ts, interval, GameObject.Find("Enemy"), this.gameObject);
        EndTurn(nextTurn);
    }

    override protected void Heal(int amt, float interval, float nextTurn)
    {
        changeHP = ScriptableObject.CreateInstance<ChangeHealth>();
        changeHP.ScheduleChange(amt, interval, this.gameObject, this.gameObject);
        EndTurn(nextTurn);
    }

    override protected void HealOverTime(int amt, int ts, float interval, float nextTurn)
    {
        changeHPOT = ScriptableObject.CreateInstance<ChangeHealthOverTime>();
        changeHPOT.ScheduleChange(amt, ts, interval, this.gameObject, this.gameObject);
        EndTurn(nextTurn);
    }

    override public void StunMe(float stunTime)
    {
        turnManager.SetNextTurn(stunTime, true);
        turnManager.SetBasicAttack(stunTime);
    }

    override protected void StunTarget(float stunTime, float nextTurn)
    {
        stun = ScriptableObject.CreateInstance<Stun>();
        stun.ScheduleStun(stunTime, GameObject.Find("Enemy"));
        EndTurn(nextTurn);
    }

    override protected void EndTurn(float next)
    {
        turnManager.SetNextTurn(next, false);
        turnManager.SetTurn(false, false);
        clock.ContGame();
    }

    //testing SPELLS BABY!
    //spellcasting stuff
    //spells need to just let go of setting next turn until the cast is complete (that is when it will actually set the next turn)
    //      this is intentional, as they will be strong
    //how to handle that? nextTurn will be set to 0.  Add a function in the turnManager that handles this
    protected void StartCasting(float time, int damage)
    {
        casting = true;
        castTime = clock.GetTime() + time;
    }
}
