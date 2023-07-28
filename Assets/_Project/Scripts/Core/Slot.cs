using System;
using System.Collections.Generic;
using _Project.Scripts.Element;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Slot : MonoBehaviour
{
	public List<MusicElement> musicElements = new();
	public static List<Slot> slots = new();
	public SoundPlayer sound;

	public UnityEvent OnEnter;
	public UnityEvent OnExit;
	
	public Elemental currentElement;
	public AudioSource audioSource;
	public AudioClip audioClip;

	private void Start() => SyncSounds();

	private void OnEnable() => slots.Add(this);

	private void OnDisable() => slots.Remove(this);

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

	private Vector3 _slotOffset = new(0, .13f, 0);

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Musicalnote")) return;
		if (!other.TryGetComponent(out Elemental elemental)) return;
		if (currentElement == elemental.Element) return;

		other.transform.DOMove(transform.position + _slotOffset, 1f);
		OnEnter?.Invoke();
		currentElement = elemental;		
		audioSource.PlayOneShot(audioClip);

		if (CinemachineShake.Instance == null) return;
		CinemachineShake.Instance.ShakeCamera(2f,.15f);

		elemental.isUsed = true;
	}
	private void OnTriggerExit2D(Collider2D other)
	{
		if (!other.CompareTag("Musicalnote")) return;
		if (!other.TryGetComponent(out Elemental elemental)) return;

		OnExit?.Invoke();
		currentElement = null;
		elemental.isUsed = false;

	}

	public string GetElementClipName() => currentElement.Element.clip.name;
}
