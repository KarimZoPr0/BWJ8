using UnityEditor;
using System.Reflection;
using UnityEngine;

static class UsefulShortcuts
{
#if UNITY_EDITOR
    [MenuItem("Tools/Editor/Clear Console %g")] // CMD + SHIFT + G
    static void ClearConsole()
    {
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
    
#endif
}