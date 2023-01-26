using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //need: player turn timer, pause the timer when turn is up, continue timer after player action.
    //          Timer object to keep an eye on time!
    public float turnInterval; //increment of turn timer
    public Timer clock; //just to track global game timer!
    private TurnManager turnManager; //local turn manager script just for this object

    // Start is called before the first frame update
    void Start()
    {
        turnManager = this.GetComponent<TurnManager>();
        turnManager.SetTurnInt(turnInterval);
        turnManager.SetNextTurn(turnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (turnManager.GetTurn())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                turnManager.SetNextTurn(turnInterval);
                turnManager.SetTurn(false);
                clock.ContGame();
            }
        }
    }
}
