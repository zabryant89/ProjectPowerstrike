using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
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
     * answer: yes
     * reason: to identify the Timer script!
     */

    //some variables
    protected GameObject target; //target of the action
    protected float time; //duration of effect (0 = instant)
    protected Timer clock; //game timer 

    private void Start()
    {
        clock = FindObjectOfType<Timer>();
    }

    //assign target for instant ability
    public void AssignTarget(GameObject targ)
    {
        target = targ;
    }

    //assign target for ability with some sort of timer
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
