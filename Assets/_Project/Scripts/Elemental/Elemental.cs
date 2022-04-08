// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using System.Collections;
using RoboRyanTron.Unite2017.Variables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace RoboRyanTron.Unite2017.Elements
{
	public class Elemental : MonoBehaviour
	{
		
		[Tooltip("Element represented by this elemental.")]
		public MusicElement Element;

		public float cubeSpeed = 10f;
 
		float directionX;
		float directionY;

		public UnityEvent OnPush;
		public UnityEvent OnRelease;

		public bool isUsed;

		private Rigidbody2D _rigidbody2D;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		[ContextMenu("Add force")]
		public void RaiseCoroutine()
		{
			StartCoroutine(MoveObj());
		}
		public IEnumerator MoveObj()
		{
			var verticalDir = Random.Range(0, 2);
			
			var isUsedDirection = verticalDir == 1 ? Vector2.up : Vector2.down;
			var direction = isUsed ? isUsedDirection : Random.insideUnitCircle.normalized;
			
			_rigidbody2D.drag = 0;
			_rigidbody2D.mass = 1;
			_rigidbody2D.AddForce(new Vector3(direction.x, direction.y).normalized * cubeSpeed);

			yield return new WaitForSeconds(1f);
			_rigidbody2D.drag = 20;
			_rigidbody2D.mass = 5;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(!other.CompareTag("Player")) return;
			if (other.GetComponent<KeyboardMover>().isMoving == false) return;
			OnPush?.Invoke();
		}
		
		private void OnTriggerExit2D(Collider2D other)
		{
			if(!other.CompareTag("Player")) return;
			OnRelease?.Invoke();
		}
		
	
	}
}