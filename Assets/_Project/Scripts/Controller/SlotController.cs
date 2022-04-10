using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{
    public List<Slot> _slots = new List<Slot>();
    private bool loop;
    public AudioSource source = new();
    public List<AudioClip> clips = new();

    private List<AudioClip> oldClips = new List<AudioClip>();
    public UnityEvent OnRightSlot;
    public UnityEvent OnWrongSlot;
    public UnityEvent OnEmptySlot;
    public UnityEvent OnSlotComplete;

    public Transform pointer;
    public List<AudioClip> emptyClips;

    // Update is called once per frame
    public IEnumerator Coroutine;

    private void Start()
    {
	    foreach (var slot in _slots)
	    {
		    oldClips.Add(slot.musicElements[0].clip);
	    }

    }
    
    

    private int index;
    private float _current, _target;

    private float duration = 0.5f;
    private float strenght = 0.5f;

    public int incremental = 0;
    public int emptyIncremental;

    public void StartCoroutine()
    {
	    StartCoroutine(Coroutine);
    }
    
    public void StopCoroutine()
    {
	    StopCoroutine(Coroutine);
    }
    
    public IEnumerator playSound()
    {
	    int index = 0;
	    int emptyIndex = 0;
	    
	    while (true)
	    {
		    if (incremental == _slots.Count - 1)
		    {
			    if (clips.SequenceEqual(oldClips))
			    {
				    OnSlotComplete?.Invoke();
				    break;
			    }
			    
			    clips.Clear();
			    
		    }


		    incremental = (index++ % _slots.Count);
		    emptyIncremental = (emptyIndex++ % emptyClips.Count);

		    var goalPosition = _slots[incremental].gameObject.transform.position.x;
		    pointer.DOMoveX(goalPosition, .4f);

		    var currentElement = _slots[incremental].currentElement.Element;
		    if (currentElement == null)
		    {
			    source.volume = 0.1f;
			    source.clip = emptyClips[emptyIncremental];
			    OnEmptySlot?.Invoke();
		    }
		    else
		    {
			    source.clip = currentElement.clip;
			    source.volume = 0.6f;
			    clips.Add(currentElement.clip);

			    if (oldClips[incremental] == currentElement.clip)
			    {
				    Debug.Log("Right");
				    OnRightSlot?.Invoke();
			    }
			    else
			    {
				    Debug.Log("Wrong");
				    OnWrongSlot?.Invoke();
			    }
		    }
		    
		    source.Play();
		    yield return new WaitForSeconds(source.clip.length);
	    }
    }
}
