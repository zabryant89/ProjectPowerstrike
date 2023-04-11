using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    //This script is to take in the actions to occur and properly time them!

    //private Action[] queue = new Action[10]; //use an array of scheduled actions (will refactor to a List later down the line)
    private List<Action> queue = new List<Action>();
    private int tail; //tail of the action queue (used to determine clean up of array!) - won't be necessary when converted to a List - NO LONGER IN USE
    private Timer clock; //get the clock!
    private float nextAction; //next action's trigger time, compared to the clock @@@ this may cause some unexpected errors.  Before testing: seems to look ok
    private int cleanUpTick; //number of ticks before we clean up the list, save speed
    private int cleanUpCount; //current count, when this equals ticks, reset and clean up list

    private void Start()
    {
        clock = FindObjectOfType<Timer>();
        nextAction = 0;
        //queue[0] = null;

        cleanUpCount = 0;
        cleanUpTick = 5400; //frames to execute before clean up, aiming for 3 or more minutes

        //testing value: 2000

        //MATH
        //1000 frames, at 30 per second = approximately every 33 seconds... nah, more
        //GOAL: aim for approximately every 2-3 minutes.
        //3 minutes = 180 seconds = 5400 frames
        //5400 frames, 30 per second = 180 seconds = 3 minutes (checked, verified, good)

        //Above is made with the following assumption: the player will execute approximately 3 turns in about that time (including NPC turns), and therefore after that many turns the list gets cleaned up.  It should also be noted this is irrelevant of the game timer and is using real time ticks.
        //Better plan... minimize use of the Update function: still using the ticks, I'm going to do this check INSIDE the CheckActions() function and if it's time: clean that mofo up!
    }

    // Update is called once per frame
    void Update()
    {
        //this MIGHT need to change - could be intensive on the computer to do this every single frame
        //possible solution: take the earliest time of the next action, then trigger at that time.
        //                      then take the array value as well, could speed things up a lot. for now... ignore
        //SOLUTION: after converting to list, simply check it every so many frames, clean it up, and move on.
        //MOVED to CheckActions()
        cleanUpCount++;

        if(nextAction != 0 && clock.GetTime() >= nextAction)
        {
            CheckActions(); //not changing the name of this method, but it's gonna fire off the next one
        }

        //Still need to check actions for when they are supposed to trigger...

        //NOTE: I have eliminated the former use of an array to list actions.
    }

    public void AddAction(Action thing)
    {
        //add the thing to the list!
        queue.Add(thing);

        //refactor 04/10/2023
        //add logic to check next skill time
        if (nextAction == 0)
            nextAction = thing.GetTime();
        else if (thing.GetTime() < nextAction)
            nextAction = thing.GetTime();

        //below is NO LONGER IN USE, comment out
        /*queue[tail] = thing;
        tail++;*/
    }

    //I no longer need this function (will be removed from its sources)
    //REASON: OrganizeActions() eliminates null values.  By using this separately, and having the actions call this function, it disrupts loops by causing "out of bounds" exceptions to be thrown.
    public void RemoveAction(Action thing)
    {
        queue.Remove(thing); //hopefully this works
    }

    private void CheckActions()
    {
        //refactor 04/10/2023
        //@@@ NEED logic somewhere in here to assign nextAction time OR set to 0 if no actions available

        for (int i = 0; i < queue.Count; i++)
        {
            //do a null check first, list may not be cleaned up yet
            if (queue[i] != null && clock.GetTime() >= queue[i].GetTime())
            {
                queue[i].DoAction();
                queue[i] = null; //the problem is this tries to destroy too many times (previously was destroy - now on the script to destroy itself)
            }
        }

        //clean up check
        if (cleanUpCount >= cleanUpTick)
        {
            cleanUpCount = 0; //reset the timer
            OrganizeActions();
        }

        //reset the action time
        nextAction = 0;

        //next action time:
        for (int i = 0; i < queue.Count; i++)
        {
            //if it exists, check it's time
            if (queue[i] != null)
            {
                //assign the time
                if (nextAction == 0)
                    nextAction = queue[i].GetTime();
                else if (queue[i].GetTime() < nextAction)
                    nextAction = queue[i].GetTime();
            }
        }

        //OrganizeActions(); now doing this on an interval.  no longer needed.
    }

    private void OrganizeActions()
    {
        //Debug.Log("Cleaning up list of actions...");
        //int count = 0; //how much to decrement the tail (no longer in use)
        //Action temp; //unecessary

        //NOTE: decrement i if it removes a value, otherwise... it won't hit the whole list!

        for (int i = 0; i < queue.Count; i++)
        {
            if (queue[i] == null)
            {
                queue.RemoveAt(i);
                i--;
            }
        }

        //FINAL NOTE: I considered a while loop here but decided against it after a couple of tests did reveal situations where null values would enter the list with gaps between active actions.
    }

    //old code for debugging, ignore
    public int ReturnActionType<T>(T type, GameObject targ)
    {
        int count = 0;
        if (queue.Count != 0)
            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i].GetType() == type.GetType() && queue[i].GetTarget() == targ)
                    count++;
            }

        return count;
    }
}
