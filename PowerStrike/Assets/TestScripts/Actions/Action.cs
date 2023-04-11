using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : ScriptableObject
{
    /* default class for an action
     * will schedule to the action queue
     * also will be extended by various other things (ie: attacks, heals, abilities, etc)
     */

    /* DESIGN
     * question: what attributes will EVERY action have?
     * answers:
     *      - target(s): entity or entities affected by the action
     *      - time: duration of the action (0 for instant use)
     *      - Timer script object: track the game timer!
     *      
     * question: is there a need for Update or Start function?
     * answer: yes, but replace start with OnEnable
     * reason: to identify the Timer script and to check the list for when an ability will fire off
     * 
     * PROBLEM: simultaneous turns cause instant actions to occur when the entity selects them, can cause issues
     * SOLUTION: add a 0.02 timer to them to ensure they occur quickly (near instant), but not to interfere with a simultaneous turn!
     * 
     * PROBLEM: simultaneous turns themselves... how should they be handled?
     * SOLUTION: continue to utilize the formula above.  abilities will trigger simultaneously, and that's ok!
     */

    //some variables
    protected GameObject owner; //owner of ability
    protected GameObject target; //target of the action
    protected float time; //duration of effect (0 = instant)
    protected Timer clock; //game timer 
    protected ActionQueue queue; //action queue

    void OnEnable()
    {
        clock = FindObjectOfType<Timer>();
        queue = GameObject.Find("ActionQueue").GetComponent<ActionQueue>();
    }

    //assign target for instant ability
    //@@@ do I need this?
    public void AssignTarget(GameObject targ)
    {
        target = targ;
    }

    //return target info (used for other things)
    public GameObject GetTarget()
    {
        return target;
    }

    //assign target for ability with some sort of timer
    //@@@ do I need this?
    public void AssignTarget(GameObject targ, float val)
    {
        target = targ;
        time = val;
    }

    public float GetTime()
    {
        return time;
    }

    public virtual void DoAction() 
    {

    } //will be defined in children
}
