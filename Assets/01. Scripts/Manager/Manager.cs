using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Manager
{
    public static LayerManager Layer { get { return LayerManager.Instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        LayerManager.ReleaseInstance();

        LayerManager.CreateInstance();
    }
}