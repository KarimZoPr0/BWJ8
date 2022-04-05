using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlotSnap : MonoBehaviour
{
	public float duration;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Musicalnote"))
		{
			other.transform.DOMove(transform.position, duration);
		}
	}
}
