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
	private void OnValidate()
	{
		if (_musicElements != null)
		{
			foreach (var musicElement in _musicElements)
			{
				sound.clips.Add(musicElement.clip);
			}
		}
	}

	public bool playableMusic;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Musicalnote")) return;
		if (!other.TryGetComponent(out Elemental elemental)) return;
		
		currentElement = elemental.Element;
		
		if (!_musicElements.Contains(currentElement))
		{
			playableMusic = false;
			renderer.color = Color.red;
			return;
		}
		
 
		playableMusic = true; 
		renderer.color = Color.green;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		
		if (!other.CompareTag("Musicalnote")) return;
		playableMusic = false;
		currentElement = null;
		renderer.color = Color.white;
	}
	
	
}
