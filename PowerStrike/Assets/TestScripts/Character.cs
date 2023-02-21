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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
