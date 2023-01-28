using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    //basic health script to manage health... straightforward!
    private int current;
    public int max;
    public TextMeshProUGUI display;

    // Start is called before the first frame update
    void Start()
    {
        current = max;
        display.text = string.Format("{0} / {1}", current, max);
    }

    // Update is called once per frame
    //potentially not necessary
    //why not check health here?
    //      answer: simple: multiple effects could jump health a lot, I would rather it destroy at the correct time!
    void Update()
    {
        
    }

    public void ChangeHealth(int val)
    {
        current += val;
        if(current > max)
        {
            current = max;
        }
        else if(current < 1)
        {
            current = 0;
            Destroy(this.gameObject);
        }

        display.text = string.Format("{0} / {1}", current, max);
    }
}
