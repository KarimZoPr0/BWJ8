using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HelpButton : MonoBehaviour
{

    public AudioSource source;
    public UnityEvent OnSongFinished;
    void Update()
    {
        if (!source.isPlaying)
        {
            OnSongFinished?.Invoke();
        }
    }

}
