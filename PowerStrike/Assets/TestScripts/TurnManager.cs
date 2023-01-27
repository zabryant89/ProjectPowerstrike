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

    // Start is called before the first frame update
    void Start()
    {
        SetTurn(false);
        scheduled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.GetTime() >= nextTurn)
        {
            clock.PauseGame();
            clock.SetTime(nextTurn);
            SetTurn(true);
        }
    }

    public void SetTurn(bool val)
    {
        turn = val;

        if (this.gameObject.name == "Player")
            clock.SetPlayer(val);
        else
            clock.SetEnemy(val);
    }

    //passing a value here as some things may change speed
    public void SetNextTurn(float next)
    {
        if (scheduled)
        {
            nextTurn += next;
        }
        else
        {
            nextTurn += next;
            scheduled = true;
        }
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
