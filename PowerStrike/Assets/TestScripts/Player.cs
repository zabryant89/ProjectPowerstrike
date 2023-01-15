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

    //next: player HP (curr/max)
    public int maxHP; //max HP for the player
    private int currHP; //current player HP
    public TextMeshProUGUI dispHP; //used for the HP display

    /*next: all player's stats
     * Brawn - physical strength
     * Agility - physical agility
     * Vigor - physical durability
     * Mind - magical ability
     */
    public int brawn;
    public int agility;
    public int vigor;
    public int mind;


    // Start is called before the first frame update
    void Start()
    {
        turn = false;
        turnTime = turnBase;
        currHP = maxHP;
        dispHP.text = string.Format("{0} / {1}", currHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        dispHP.text = string.Format("{0} / {1}", currHP, maxHP);
        if (clock.GetTime() >= turnTime)
        {
            clock.PauseGame();
            Debug.Log("Begin PLAYER turn");
            turn = true;
        }

        if (turn && Input.GetKey(KeyCode.W))
        {
            turnTime = turnBase + clock.GetTime();
            clock.ContGame();
            //currHP -= 1;
            turn = false;
            Debug.Log("End PLAYER turn");
        }
    }

    public void EndTurn()
    {

    }
}
