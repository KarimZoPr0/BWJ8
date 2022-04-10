using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
	private void Awake()
	{
		var sceneTransitor = Instantiate(new GameObject(), transform.position, Quaternion.identity, transform);
		sceneTransitor.name = "Scenes Manager";
		sceneTransitor.AddComponent<SceneTransitor>();
	}
}
