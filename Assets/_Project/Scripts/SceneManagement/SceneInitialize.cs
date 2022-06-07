using UnityEngine;

namespace _Project.Scripts.SceneManagement
{
    public static class SceneInitialize
    {
        private const string Scene = "_BootLoader";
    
        [RuntimeInitializeOnLoadMethod]
        static void OnProjectLoadedInEditor()
        {
            if (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(Scene).IsValid())
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(Scene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            }
        }
    }
}
