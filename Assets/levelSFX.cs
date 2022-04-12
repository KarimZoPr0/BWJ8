using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public AudioClip click;

    private int clipNo;
    void Start()
    {
        audioSource.clip = audioClips[Random.Range(0, 2)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mouseEnterSFX()
    {
        clipNo = Random.Range(0, 2);
        audioSource.PlayOneShot(audioClips[clipNo], .5f);
        
    }

    public void clickSFX()
    {
        audioSource.PlayOneShot(click, .5f);
    }
}
