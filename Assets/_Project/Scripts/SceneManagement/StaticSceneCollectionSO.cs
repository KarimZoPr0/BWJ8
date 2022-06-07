using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using _Project.Scripts.SceneManagement;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "SceneEvents/StaticSceneCollectionSO")]
public class StaticSceneCollectionSO : ScriptableObject
{
	[Header("Information")] 
	public List<SceneAsset> staticScenes;


}
