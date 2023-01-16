// Intent: handle health for all entities requiring it

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    //standard globals:
    private int current; //current hp
    private int max; //max hp
    public TextMeshProUGUI dispHP; //used for the HP display

    //DoTs and HoTs
    //DID NOT LIKE the original idea.  Going to implement something new.  See devlog
    //moved to StatusEffects script

    // Start is called before the first frame update
    void Start()
    {
        current = max;
    }

    // Update is called once per frame
    void Update()
    {
        dispHP.text = string.Format("{0} / {1}", current, max);
        if (current > max)
            current = max;
    }

    //this is for initial setting of maxhp only!
    public void SetMaxHP(int val)
    {
        max = val;
        current += val; 
    }

    public int GetMaxHealth()
    {
        return max;
    }

    /*how to use:
     * pass negative values to damage
     * pass positive values to heal
     */
    public void ChangeHealth(int val)
    {
        current += val;
        //no negative values allowed
        if (current < 0)
            current = 0;
    }
}
