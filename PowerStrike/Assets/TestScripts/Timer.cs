using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    /* Design note
     * Real time will never stop, will always run
     * To simulate turn ticks, we will use a "timeline" system.
     *      
     */
    //need: current time, paused bool, textmeshpro object to change text on screen

    private float curTime;
    private float futureTime;
    private bool paused;
    public TextMeshProUGUI timerText;
    

    
    // Start is called before the first frame update
    void Start()
    {
        curTime = 0f;
        futureTime = curTime + 10f;
        paused = false;
        //timerText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //pause check, if false: continue the timer
        if (!paused)
        {
            timerText.text = string.Format("{0:0.00}", curTime) + string.Format("    {0:0.00}", futureTime);
            curTime += Time.deltaTime;
            futureTime = curTime + 10f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ContGame();
        }
    }

    //can do one method for the pausing/unpausing, however for now I want direct control.
    public void PauseGame()
    {
        paused = true;
    }

    public void ContGame()
    {
        //while (playerBlock || npcBlock) ;
        paused = false;
    }

    //for other scripts to access the current time
    public float GetTime()
    {
        return curTime;
    }
}
