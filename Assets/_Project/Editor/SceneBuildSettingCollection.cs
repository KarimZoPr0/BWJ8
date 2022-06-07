using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SceneBuildSettingsCollection : EditorWindow
{
	List<SceneAsset> m_SceneAssets = new List<SceneAsset>();

	// Add menu item named "Example Window" to the Window menu
	[MenuItem("Tools/Scene Collection")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		GetWindow(typeof(SceneBuildSettingsCollection));
	}

	void OnGUI()
	{
		GUILayout.Label("Scenes to include in build:", EditorStyles.boldLabel);
		for (int i = 0; i < m_SceneAssets.Count; ++i)
		{
			m_SceneAssets[i] = (SceneAsset)EditorGUILayout.ObjectField(m_SceneAssets[i], typeof(SceneAsset), true);
		}
		if (GUILayout.Button("Add"))
		{
			m_SceneAssets.Add(null);
		}
		
		if (GUILayout.Button("Delete"))
		{
			if (m_SceneAssets == null) return;
			m_SceneAssets.RemoveAt(m_SceneAssets.Count - 1);
		}
		if (GUILayout.Button("Clear"))
		{
			m_SceneAssets = new List<SceneAsset>();
		}

		GUILayout.Space(12);

		if (GUILayout.Button("Apply To Build Settings"))
		{
			SetEditorBuildSettingsScenes();
		}
		if (GUILayout.Button("Clear Build Settings"))
		{

			EditorBuildSettings.scenes = null;
		}
	}

	public void SetEditorBuildSettingsScenes()
	{
		// Find valid Scene paths and make a list of EditorBuildSettingsScene
		List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
		foreach (var sceneAsset in m_SceneAssets)
		{
			string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
			if (!string.IsNullOrEmpty(scenePath))
			{
				editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
			}
		}
		
		foreach (var scene in EditorBuildSettings.scenes)
		{
			if (!editorBuildSettingsScenes.Contains(scene))
			{
				editorBuildSettingsScenes.Add(scene);
			}
		}

		// Set the Build Settings window Scene list
		EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
	}
}