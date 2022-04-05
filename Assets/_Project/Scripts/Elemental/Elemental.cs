// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
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
			var direction = Random.insideUnitCircle.normalized;

			_rigidbody2D.drag = 0;
			_rigidbody2D.AddForce(new Vector3(direction.x, direction.y).normalized * cubeSpeed);

			yield return new WaitForSeconds(2f);
			_rigidbody2D.drag = 20;
		}
	}
}