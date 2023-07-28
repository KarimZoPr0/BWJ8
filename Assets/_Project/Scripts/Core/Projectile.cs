using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
	public Transform target;
	public Rigidbody2D rigidBody;
	public float angleChangingSpeed;
	public float movementSpeed;
	public AudioSource audioSource;
	public AudioClip damageSFX;

	public UnityEvent OnHit;
	public float knockBack;

	private void OnBecameInvisible()
	{
		Debug.Log("IsOut");
	}

	public bool canTurn = true;
	
	void FixedUpdate ()
	{
		if (target == null) return;
		if(canTurn)
		{
			StartCoroutine(Rotate());
		}
		
		rigidBody.velocity = -transform.up * movementSpeed;
	}


	IEnumerator Rotate()
	{
		Vector2 direction = (Vector2) target.position - rigidBody.position;
			
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, -transform.up).z;
		rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;

		yield return new WaitForSeconds(1.5f);
		canTurn = false;
		rigidBody.angularVelocity = 0;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Player")) return;

		OnHit?.Invoke();
		
		Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
		Vector3 direction = hit.position - rigidBody.position;
		
		hit.AddRelativeForce(direction * knockBack);
		hit.velocity = Vector2.zero;

		
		Destroy(gameObject);

	}
}
