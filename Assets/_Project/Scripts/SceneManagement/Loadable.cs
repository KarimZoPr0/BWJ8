using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Project.Scripts.SceneManagement
{
	public abstract class Loadable : ScriptableObject
	{
		public List<SceneAsset> finalScenes;
	}
}