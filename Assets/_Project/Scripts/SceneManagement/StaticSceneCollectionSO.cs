using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using _Project.Scripts.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SceneEvents/StaticSceneCollectionSO")]
public class StaticSceneCollectionSO : ScriptableObject
{

	[Header("Information")] 
	public List<SceneReference> staticScenes;
	
	public StaticSceneCollectionSO(SceneReference sceneReference)
	{
		staticScenes.Add(sceneReference);
	}
}
