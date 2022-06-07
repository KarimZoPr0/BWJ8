using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
	public float speed = 100f;

	private Vector3 _rotation;


	private void Start()
	{
		_rotation = new Vector3(0, 0, speed);
	}

	private void Update ()
	{
		transform.Rotate(_rotation * Time.deltaTime);
	}
}
