using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Editor
{
    public enum ScenesCollection
    {
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        LevelSelector,
    }
    
    
    public class SceneCollectionLoader : EditorWindow
    {
        public ActiveSceneCollectionSO currentScene;
        public static List<Scene> scenesToUnLoad = new List<Scene>();
        public ScenesCollection scenesCollection; 
        [MenuItem("Tools/Loader Collection")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            GetWindow(typeof(SceneCollectionLoader));
        }
        void OnGUI()
        {
            scenesCollection = (ScenesCollection)EditorGUILayout.EnumPopup("SceneCollection to load:", scenesCollection);
            if (GUILayout.Button("Load"))
            {
                UnloadAllScenes();
                FindSceneCollection("/Levels");
                LoadSceneCollection();
            }
        }

        private void LoadSceneCollection()
        {
            foreach (var finalScene in currentScene.finalScenes)
            {
                string scenePath = AssetDatabase.GetAssetOrScenePath(finalScene);
                EditorSceneManager.OpenScene($"{scenePath}", OpenSceneMode.Additive);
            }
        }

        private void FindSceneCollection(string additionalPath)
        {
            string[] assetNames =
                AssetDatabase.FindAssets("Level", new[] {"Assets/_Project/Scriptables/SceneManagement/Scenes" + additionalPath});
            foreach (var result in assetNames)
            {
                var SOpath = AssetDatabase.GUIDToAssetPath(result);
                var scene = AssetDatabase.LoadAssetAtPath<ActiveSceneCollectionSO>(SOpath);
                if (scene.name[^1] == scenesCollection.ToString()[scenesCollection.ToString().Length - 1])
                {
                    currentScene = scene;
                    Debug.Log(currentScene.name);
                }
            }
        }

        private void UnloadAllScenes()
        {
            AddToUnload();
            foreach (var scene in scenesToUnLoad)
            {
                EditorSceneManager.UnloadSceneAsync(scene);
            }
        }

        public void AddToUnload()
        {
            scenesToUnLoad.Clear();
            
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                scenesToUnLoad.Add(scene);
            }
        }
        
        private void LoadScenes()
        {
           
        }
    }
}