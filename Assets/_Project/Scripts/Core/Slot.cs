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
	public AudioSource audioSource;
	public AudioClip audioClip;


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

		OnEnter?.Invoke();
		currentElement = elemental.Element;
		elemental.isUsed = true;
		audioSource.PlayOneShot(audioClip);

		CinemachineShake.Instance.ShakeCamera(2f,.15f);
	}
	
	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("Musicalnote")) return;
		if (!other.TryGetComponent(out Elemental elemental)) return;

		OnExit?.Invoke();
		currentElement = null;
		elemental.isUsed = false;
	}
	
	
}
