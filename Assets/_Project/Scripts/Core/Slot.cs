using System;
using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Elements;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
	public List<MusicElement> musicElements = new();
	public SoundPlayer sound;

	public UnityEvent OnEnter;
	public UnityEvent OnExit;
	
	public MusicElement currentElement;


	private void Start()
	{
		SyncSounds();
	}

	private void SyncSounds()
	{
		if (musicElements == null) return;
		sound.clips.Clear();
		
		if (sound.clips.Count == musicElements.Count && sound.clips.Count != 0) return;
	
		
		foreach (var musicElement in musicElements)
		{
			sound.clips.Add(musicElement.clip);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Musicalnote")) return;
		if (!other.TryGetComponent(out Elemental elemental)) return;

		OnExit?.Invoke();
		currentElement = elemental.Element;
		elemental.isUsed = true;

	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("Musicalnote")) return;
		if (!other.TryGetComponent(out Elemental elemental)) return;

		OnEnter?.Invoke();
		currentElement = null;
		elemental.isUsed = false;
	}
	
	
}
