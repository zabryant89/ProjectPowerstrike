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
            StartCoroutine(SimulateTurn(4f));
        }
    }

    //simulates the NPC turn
    private IEnumerator SimulateTurn(float time)
    {
        temp.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        waiting = false;
        turnManager.SetNextTurn(turnInterval);
        turnManager.SetTurn(false);
        clock.ContGame();
        temp.gameObject.SetActive(false);
    }
}
