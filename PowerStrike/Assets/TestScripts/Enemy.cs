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
    private bool acting; //need this for NPC's to handle their turn timers
    public Timer clock; //check/manipulate the timer as needed

    // Start is called before the first frame update
    void Start()
    {
        turnTime = baseTime;
        turn = false;
        acting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.GetTime() >= turnTime && !acting && !clock.GetPlayerBlock())
        {
            clock.SetNPCBlock(true);
            clock.PauseGame();
            turn = true;
            turnTime = baseTime + clock.GetTime();
        }

        if (turn)
        {
            StartCoroutine(Act());
        }
    }

    //temporary actions
    public IEnumerator Act()
    {
        while (clock.GetPlayerBlock()) ;
        Debug.Log("Begin ENEMY turn");
        acting = true;
        turn = false;
        yield return new WaitForSeconds(2f);
        acting = false;
        clock.SetNPCBlock(false);
        clock.ContGame();
        Debug.Log("End ENEMY turn");
    }
}
