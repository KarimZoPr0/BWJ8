using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Inro : MonoBehaviour
{
    // Update is called once per frame
    public PlayableDirector director;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            director.Play();
        }
    }
}
