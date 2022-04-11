using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public AudioClip clickSFX;

    private int clipNo = 0;
    void Start()
    {
        clipNo = Random.Range(0, 2);
        audioSource.clip = audioClips[clipNo];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mouseEnterSFX()
    {
        audioSource.PlayOneShot(audioClips[clipNo], .5f);
        clipNo = Random.Range(0, 2); 
        if (clipNo > 2)
        {
            clipNo = 0;
        }
    }

    public void playClickSFX()
    {
        audioSource.clip = clickSFX;
        audioSource.PlayOneShot(clickSFX, .5f);
    }
}
