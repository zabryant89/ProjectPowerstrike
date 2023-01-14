using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //need: current time, paused bool, textmeshpro object to change text on screen

    private float curTime;
    private bool paused;
    public TextMeshProUGUI timerText;

    
    // Start is called before the first frame update
    void Start()
    {
        curTime = 0f;
        paused = false;
        //timerText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //pause check, if false: continue the timer
        if (paused == false)
        {
            timerText.text = string.Format("{0:0.00}", curTime);
            curTime += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)){
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.S))
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
        paused = false;
    }

    //for other scripts to access the current time
    public float GetTime()
    {
        return curTime;
    }
}
