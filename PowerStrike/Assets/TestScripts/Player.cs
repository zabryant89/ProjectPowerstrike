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
    private Damage basicAttack; //basic attack
    private Damage doubleAttack; //second of a double attack (trying something here)

    // Start is called before the first frame update
    void Start()
    {
        //going to test this inside Update first... may not work.. might? Idk
        //basicAttack = new Damage();
        //basicAttack.AssignTarget(GameObject.FindGameObjectWithTag("Enemy"));
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
                EndTurn();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                //going to test two damages here: instant and one that is delayed!
                //instant
                BasicAttack(10);
                //delayed testing inside basic attack (just one change rather than a whole new method!
                //NOW to test a double attack...
            }
        }
    }

    private void BasicAttack(int dmg)
    {
        basicAttack = ScriptableObject.CreateInstance<Damage>();
        doubleAttack = ScriptableObject.CreateInstance<Damage>();
        //basicAttack.AssignTarget(GameObject.Find("Enemy")); //derp... unecessary
        basicAttack.ScheduleDamage(10, 0f, GameObject.Find("Enemy")); //first of two
        doubleAttack.ScheduleDamage(10, 1f, GameObject.Find("Enemy")); //second of two
        //Destroy(basicAttack); //DO NOT DO THIS HERE, ruins the entier action queue
        EndTurn();
    }

    private void EndTurn()
    {
        turnManager.SetNextTurn(turnInterval);
        turnManager.SetTurn(false);
        clock.ContGame();
    }
}
