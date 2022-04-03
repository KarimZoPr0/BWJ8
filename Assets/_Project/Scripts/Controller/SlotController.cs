using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{
    public List<Slot> _slots = new List<Slot>();
    private bool loop;
    public AudioSource source = new();
    public List<AudioClip> clips = new();

    public UnityEvent EmptySlot;

    public Transform pointer;

    // Update is called once per frame

    private void Start()
    {
	    StartCoroutine(PlayMusic());
    }

    public int index;

    private float _current, _target;

    private float duration = 0.5f;
    private float strenght = 0.5f;
    IEnumerator PlayMusic()
    {
	    for (index = 0; index < _slots.Count;)
	    {
		    var goalPosition = _slots[index].gameObject.transform.position.x;
		    pointer.DOShakeRotation(duration, strenght);
		    pointer.DOShakeScale(duration, strenght);
		    
		    pointer.DOMoveX(goalPosition, .4f);
		    
		    if (_slots[index].currentElement == null)
		    {
			    source.PlayOneShot(clips[0]);
		    }
		    else
		    {
			    source.PlayOneShot(_slots[index].currentElement.clip);
		    }
		  
		    
		    while (source.isPlaying)
		    { 
			   
			    yield return null;
		    }
		    index++;
		   
		    if (index == _slots.Count)
		    { 
			    index = 0;
		    }

	    }
	    
    }
}
