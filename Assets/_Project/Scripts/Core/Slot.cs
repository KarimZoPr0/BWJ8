using System;
using System.Collections;
using System.Collections.Generic;
using RoboRyanTron.Unite2017.Elements;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
	public List<MusicElement> _musicElements = new();
	public SpriteRenderer renderer;
	public SoundPlayer sound;


	public MusicElement currentElement;

	private void Start()
	{
		SyncSounds();
	}

	[ContextMenu("SyncSoundsFromElements")]
	public void SyncSounds()
	{
		if (_musicElements == null) return;
		sound.clips.Clear();
		
		if (sound.clips.Count == _musicElements.Count && sound.clips.Count != 0) return;
	
		
		foreach (var musicElement in _musicElements)
		{
			sound.clips.Add(musicElement.clip);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Musicalnote")) return;
		if (!other.TryGetComponent(out Elemental elemental)) return;
		
		currentElement = elemental.Element;
		
		if (!_musicElements.Contains(currentElement))
		{
			renderer.color = Color.red;
			return;
		}
		
		renderer.color = Color.green;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		
		if (!other.CompareTag("Musicalnote")) return;
		currentElement = null;
		renderer.color = Color.white;
	}
	
	
}
