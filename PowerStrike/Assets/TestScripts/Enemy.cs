using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //the following copy/paste from the player script... maybe should've made a "turn manager"
    public float turnInterval; //increment of turn timer
    private float nextTurn; //actual time to pause for player's turn
    private bool scheduled; //marks if the next turn has been scheduled or not, important for "turn alteration" effects!
    private bool turn; //is it the player's turn? (used to determine if actions can be taken or not)
    public Timer clock; //just to track global game timer!
    public Player player; //simply to check for turn boolean

    // Start is called before the first frame update
    void Start()
    {
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
            StartCoroutine(TakeAction()); //temporary function
        }
    }

    private IEnumerator TakeAction()
    {
        if (player.GetTurn())
        {
            StartCoroutine(WaitForPlayer());
        }
        else
        {
            yield return new WaitForSeconds(4f);
            SetNextTurn(turnInterval);
            SetTurn(false);
            clock.ContGame();
        }
    }

    private IEnumerator WaitForPlayer()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(TakeAction());
    }

    private void SetTurn(bool val)
    {
        turn = val;
    }

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
}
