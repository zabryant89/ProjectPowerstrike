using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    //This script is to take in the actions to occur and properly time them!

    private Action[] queue = new Action[10]; //use an array of scheduled actions (will refactor to a List later down the line)
    private int tail; //tail of the action queue (used to determine clean up of array!) - won't be necessary when converted to a List
    private Timer clock; //get the clock!

    private void Start()
    {
        clock = FindObjectOfType<Timer>();
        queue[0] = null;
    }

    // Update is called once per frame
    void Update()
    {
        //this MIGHT need to change - could be intensive on the computer to do this every single frame
        //possible solution: take the earliest time of the next action, then trigger at that time.
        //                      then take the array value as well, could speed things up a lot. for now... ignore
        if (queue[0] != null)
        {
            CheckActions();
        }
    }

    public void AddAction(Action thing)
    {
        queue[tail] = thing;
        tail++;
    }

    private void CheckActions()
    {
        for (int i = 0; i < tail; i++)
        {
            if (clock.GetTime() >= queue[i].GetTime())
            {
                queue[i].DoAction();
                queue[i] = null; //the problem is this tries to destroy too many times (previously was destroy - now on the script to destroy itself)
            }
        }

        OrganizeActions();
    }

    private void OrganizeActions()
    {
        int count = 0; //how much to decrement the tail.
        //Action temp; //unecessary
        for (int i = 0; i < tail - 1; i++)
        {
            if (queue[i] == null)
            {
                //temp = queue[i];
                queue[i] = queue[i + 1];
                queue[i + 1] = null;
                //count++;
            }
        }

        //properly find tail, this was the issue (FIXED DAMAGE BUG)
        if (queue[0] == null)
            tail = 0;
        else
        {
            int i = 0;
            while (i < queue.Length && queue[i] != null)
            {
                i++;
                count++;
            }

            tail = count;
        }
    }

    //old code for debugging, ignore
    public int ReturnActionType<T>(T type, GameObject targ)
    {
        int count = 0;
        if (queue.Length != 0)
            for (int i = 0; i < queue.Length; i++)
            {
                if (queue[i].GetType() == type.GetType() && queue[i].GetTarget() == targ)
                    count++;
            }

        return count;
    }
}
