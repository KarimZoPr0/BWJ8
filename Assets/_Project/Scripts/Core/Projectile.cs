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

	public UnityEvent OnHit;

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	public bool canTurn = true;

	void FixedUpdate ()
	{
		if (target == null) return;
		if(canTurn)
		{
			StartCoroutine(Rotate());
		}
		
		rigidBody.velocity = transform.up * movementSpeed;
	}


	IEnumerator Rotate()
	{
		Vector2 direction = (Vector2) target.position - rigidBody.position;
			
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;

		yield return new WaitForSeconds(3);
		canTurn = false;
		rigidBody.angularVelocity = 0;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;
		OnHit?.Invoke();
		Destroy(gameObject);
	}
}
