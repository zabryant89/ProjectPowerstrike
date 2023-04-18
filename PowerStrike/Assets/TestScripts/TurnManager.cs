using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //created this script to deal with managing turns for each object
    //will save me a lot of repeated code.  Most of this is copy/paste from Player/Enemy scripts
    private float turnInterval; //increment of turn timer
            //I'm not sure if I even need the turnInterval variable in this script...
    private float nextTurn; //actual time to pause for entity's turn
    private bool scheduled; //marks if the next turn has been scheduled or not, important for "turn alteration" effects!
    private bool turn; //is it the entity's turn? (used to determine if actions can be taken or not)
    public Timer clock; //just to track global game timer!

    //realizing now that turn manager needs to handle basic attacks.
    //NOTE: must loop infinitely until combat ends, and freeze when stunned!
    //Below are the basic attack globals:
    private Character entity;
    private float nextSwing; //next basic attack swing

    // Start is called before the first frame update
    void Start()
    {
        SetTurn(false, false);
        //scheduled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (scheduled && nextTurn != 0 && clock.GetTime() >= nextTurn)
        {
            clock.PauseGame();
            clock.SetTime(nextTurn);
            SetTurn(true, true);
        }
        else
        {
            Debug.Log("Scheduled bool at time " + clock.GetTime() + " --> " + scheduled);
        }
    }

    public void SetEntity(Character play)
    {
        entity = play;
    }

    public void SetTurn(bool val, bool turnStart)
    {
        turn = val;

        if (turnStart)
            scheduled = false;

        if (this.gameObject.name == "Player")
            clock.SetPlayer(val);
        else
            clock.SetEnemy(val);
    }

    //passing a value here as some things may change speed
    public void SetNextTurn(float next, bool crowdControl)
    {
        if (next == 0)
        {
            nextTurn = next;
            scheduled = false;
        }
        else
        {
            nextTurn += next;
            scheduled = true;
        }
    }

    //basic attack stuff:
    public void SetBasicAttack(float next)
    {
        nextSwing += next;
    }

    public void SetTurnInt(float val)
    {
        turnInterval = val;
    }

    public bool GetTurn()
    {
        return turn;
    }
}
