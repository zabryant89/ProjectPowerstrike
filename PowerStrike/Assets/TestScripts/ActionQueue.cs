using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    /*
     * This script is to have the NEXT upcoming actions occur on their time stamp
     * Challenge: have ONLY the next event in an over-time or multiple tick effect occur and in the list
     *            and do not let multiple future ticks of an event show in the list at any time
     */
    //2 arrays (because the types are going to HAVE to be different) to manage: [event time trigger][event]
    //          first array will reference the occurrence of it's related event
    //          the second array contains the actual event (referenced by it's type)
    //TEST: can you use the "generic" type? let's find out!
    //          cannot (well can... but): requires a class of generic type, might mess later, for now: dumb way!

    private float[] eventTime = new float[10]; 
    private GameObject[] eventObj = new GameObject[10]; //will use GameObject to reference it's event

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
