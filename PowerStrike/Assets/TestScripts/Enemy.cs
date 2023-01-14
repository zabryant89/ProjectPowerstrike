using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //NOTE: this MUST be a general thing for enemies! We will implement inheritance to tackle enemy specifics
    //all enemies need: hp, turn timers
    public float baseTime; //turn interval
    private float turnTime; //when the timer needs to pause for enemy turn
    private bool turn; //is it this character's turn?
    public Timer clock; //check/manipulate the timer as needed

    // Start is called before the first frame update
    void Start()
    {
        turnTime = baseTime;
        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.GetTime() >= turnTime)
        {
            turn = true;
            clock.PauseGame();
            turnTime = baseTime + clock.GetTime();
        }

        if (turn == true)
        {
            Act();
        }
    }

    //temporary actions
    public void Act()
    {
        
    }

    public I
}
