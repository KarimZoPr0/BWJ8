using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
	public Transform target;
	public Rigidbody2D rigidBody;
	public float angleChangingSpeed;
	public float movementSpeed;
	public AudioSource audioSource;
	public AudioClip damageSFX;

	public UnityEvent OnHit;

	private void OnBecameInvisible()
	{
		Debug.Log("Invisble");
		enabled = false;
	}

	private void OnBecameVisible()
	{
		Debug.Log("Visible");
	}

	public bool canTurn = true;

	void FixedUpdate ()
	{
		if (target == null) return;
		if(canTurn)
		{
			StartCoroutine(Rotate());
		}
		
		rigidBody.velocity = transform.right * movementSpeed;
	}


	IEnumerator Rotate()
	{
		Vector2 direction = (Vector2) target.position - rigidBody.position;
			
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.right).z;
		rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;

		yield return new WaitForSeconds(3);
		canTurn = false;
		rigidBody.angularVelocity = 0;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;
		OnHit?.Invoke();
		audioSource.PlayOneShot(damageSFX);
		CinemachineShake.Instance.ShakeCamera(3f,.25f);
	}
}
