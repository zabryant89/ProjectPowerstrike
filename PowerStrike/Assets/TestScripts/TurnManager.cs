using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //created this script to deal with managing turns for each object
    //will save me a lot of repeated code.  Most of this is copy/paste from Player/Enemy scripts
    public float turnInterval; //increment of turn timer
    private float nextTurn; //actual time to pause for player's turn
    private bool scheduled; //marks if the next turn has been scheduled or not, important for "turn alteration" effects!
    private bool turn; //is it the player's turn? (used to determine if actions can be taken or not)
    public Timer clock; //just to track global game timer!

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetTurn(bool val)
    {
        turn = val;
        clock.SetPlayer(val);
    }

    //passing a value here as some things may 
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

    public bool GetTurn()
    {
        return turn;
    }
}
