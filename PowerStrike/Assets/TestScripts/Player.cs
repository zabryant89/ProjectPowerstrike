using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    //need: player turn timer, pause the timer when turn is up, continue timer after player action.
    //          Timer object to keep an eye on time!
    public float turnBase; //increment of turn timer
    private float turnTime; //actual time to pause for player's turn
    private bool turn; //is it the player's turn? (used to determine if actions can be taken or not)
    public Timer clock; //just to track global game timer!

    // Start is called before the first frame update
    void Start()
    {
        turn = false;
        turnTime = turnBase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
