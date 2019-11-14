using UnityEngine;
using UnityEditor;
using System;

public class MyEditor : EditorWindow
{
    public float _sliderValue;
    public string _textValue;
    public float x, y, w, h;
    public Vector2 _scroll;
    public float height =55f;

    [MenuItem("TestEditor/My Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<MyEditor>("맵 에디터");
    }

    private void OnGUI()
    {
        // 배경색 입히기 꾸미기
        //Color prevColor = GUI.color;
        //GUI.color = Color.yellow;
        //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a box");
        //GUI.color = prevColor;

        // GUI의 외향 바꾸기
        //GUIStyle guiStyle_Box = new GUIStyle(GUI.skin.box);
        //guiStyle_Box.alignment = TextAnchor.MiddleCenter;
        //GUILayout.Box("..ggggggggggggg.", guiStyle_Box);

        // Begin ~ End View
        _scroll = EditorGUILayout.BeginScrollView(_scroll); //Vector2 리턴
        EditorGUILayout.BeginVertical();

        // 레이아웃 그룹 내의 GUI 코드

        EditorGUILayout.EndVertical();
        
        GUILayout.Space(height);

        EditorGUILayout.EndScrollView();


        //_sliderValue = GUI.HorizontalScrollbar(new Rect(10, 10, 300, 20), _sliderValue, 10, 0, 100);

        //GUILayout.Space(30);

        //if (GUILayout.Button("Enter"))
        //{
        //    Debug.Log("Hello World!");
        //}

        //EditorGUILayout.Space();
        //_textValue = EditorGUILayout.DelayedTextField("Text", _textValue);
    }
}
