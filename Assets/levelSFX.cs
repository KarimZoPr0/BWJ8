using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private int clipNo = 0;
    void Start()
    {
        audioSource.clip = audioClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mouseEnterSFX()
    {
        audioSource.PlayOneShot(audioClips[clipNo], .5f);
        clipNo += 1; 
        if (clipNo > 2)
        {
            clipNo = 0;
        }
    }
}
