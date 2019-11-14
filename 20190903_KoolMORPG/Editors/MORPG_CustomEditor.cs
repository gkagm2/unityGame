using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        return false;
    }
}

[CustomEditor(typeof(MORPG_CustomEditor))]
public class MORPG_CustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("에디터 열기"))
        {
            MORPG_WinEditor.ShowWindow((MORPG_GameData)target);
        }
    }
}
