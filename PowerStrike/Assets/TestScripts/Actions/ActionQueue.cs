using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    //This script is to take in the actions to occur and properly time them!

    private Action[] queue = new Action[10]; //use an array of scheduled actions
    private int tail; //tail of the action queue (used to determine clean up of array!)
    private Timer clock; //get the clock!

    private void Start()
    {
        clock = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
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
            }
        }
    }
}
