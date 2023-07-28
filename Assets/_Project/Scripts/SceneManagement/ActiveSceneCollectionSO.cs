using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This class is a base class which contains what is common to all game scenes (Locations or Menus)
/// </summary>

[CreateAssetMenu(menuName = "SceneEvents/ActiveSceneCollectionSO")]

public class ActiveSceneCollectionSO : ScriptableObject
{

	[Header("Collection Scene Settings")] 
	[Tooltip("Scenes that will be added together to create the final scene")]
	public StaticSceneCollectionSO staticSceneCollection;

	[Header("Information")] 
	public List<SceneReference> activeScenes;
	public string shortDescription;
	
	[HideInInspector]
	public List<SceneReference> finalScenes = new();
	[Header("Audio")]
	public AudioClip levelMusic;
	
	public void OnValidate()
	{
		finalScenes.Clear();
		
		if (activeScenes.Count == 0)
		{
			Debug.LogError("Scene Name is empty for " + this.name);
		}
		
		if (activeScenes != null)
		{
			var difference = activeScenes.Except(finalScenes);
			finalScenes.AddRange(difference);
		}


		if (staticSceneCollection != null)
		{
			var difference = staticSceneCollection.staticScenes.Except(finalScenes);
			finalScenes.AddRange(difference);
		}
		
	}
	
}