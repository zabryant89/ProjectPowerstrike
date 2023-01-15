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

    //next: player HP (curr/max) (@@@will need to remove later)
    public int maxHP; //max HP for the player
    public Health hp;
    /*private int currHP; //current player HP
    public TextMeshProUGUI dispHP; //used for the HP display*/


    /*next: all player's stats
     * Brawn - physical strength
     * Agility - physical agility
     * Vigor - durability
     * Mind - magical ability
     */
    public int brawn;
    public int agility;
    public int vigor;
    public int mind;


    // Start is called before the first frame update
    void Start()
    {
        hp.SetMaxHP(maxHP);
        turn = false;
        turnTime = turnBase;
    }

    // Update is called once per frame
    void Update()
    {
        //dispHP.text = string.Format("{0} / {1}", currHP, maxHP);
        if (clock.GetTime() >= turnTime && !clock.GetNPCBlock())
        {
            Debug.Log("Begin PLAYER turn");
            clock.SetPlayerBlock(true);
            clock.PauseGame();
            turnTime = turnBase + clock.GetTime();
            turn = true;
        }

        if (turn && Input.GetKey(KeyCode.A))
        {
            turn = false;
            BasicAttack();
        }
        else if (turn && Input.GetKey(KeyCode.W))
        {
            turn = false;
            Bleed();
        }
    }

    public void EndTurn()
    {
        while (clock.GetNPCBlock()) ;
        clock.SetPlayerBlock(false);
        clock.ContGame();
        //currHP -= 1;
        Debug.Log("End PLAYER turn");
    }

    private void BasicAttack()
    {
        GameObject enemy = GameObject.Find("Enemy");
        Health enemyHP = enemy.GetComponent<Health>();
        enemyHP.ChangeHealth(-8);
        EndTurn();
    }

    private void Bleed()
    {
        /*GameObject enemy = GameObject.Find("Enemy");
        Health enemyHP = enemy.GetComponent<Health>();
        enemyHP.ChangeHealthOverTime(2f, 4, 5);
        EndTurn();*/
    }
}
