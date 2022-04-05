using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musician : MonoBehaviour
{
    public EvilBlock evilBlock;
    public List<GameObject> blocks;
    private void Start()
    {

        // Perform the action every 3 seconds
       // InvokeRepeating("DoSomething", 5f, 5f);
    }


    public void DoSomething()
    {
        Debug.Log("Perform Action");
    }

    // Update is called once per frame
    void Update()
    {
        
        // Play an animation
        
        // Play sounds
        
        // Perform an action once a certain time has been reached
        
    }
}
