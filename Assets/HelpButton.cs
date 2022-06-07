using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HelpButton : MonoBehaviour
{

    public AudioSource source;
    public UnityEvent OnSongFinished;
      
    public bool isInvoked;
    
    public void SetInvokedTrue()
    {
        isInvoked = false;
    } 
    
    void Update()
    {
        if (!source.isPlaying && !isInvoked)
        {
            OnSongFinished.Invoke();
        }
    }
}
