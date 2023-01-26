using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //need: player turn timer, pause the timer when turn is up, continue timer after player action.
    //          Timer object to keep an eye on time!
    public float turnInterval; //increment of turn timer
    private float nextTurn; //actual time to pause for player's turn
    private bool scheduled; //marks if the next turn has been scheduled or not, important for "turn alteration" effects!
    private bool turn; //is it the player's turn? (used to determine if actions can be taken or not)
    public Timer clock; //just to track global game timer!
    private TurnManager turnManager; //local turn manager script just for this object

    // Start is called before the first frame update
    void Start()
    {
        turnManager = this.GetComponent<TurnManager>();
        turn = false;
        SetNextTurn(turnInterval);
        scheduled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.GetTime() >= nextTurn)
        {
            clock.PauseGame();
            clock.SetTime(nextTurn);
            SetTurn(true);
            scheduled = false;
        }

        if (turn)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                SetNextTurn(turnInterval);
                SetTurn(false);
                clock.ContGame();
            }
        }
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
