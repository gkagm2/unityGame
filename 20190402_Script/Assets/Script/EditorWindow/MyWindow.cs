using UnityEngine;
using UnityEditor;

public class MyWindow : EditorWindow {

    string myString = "Hello world";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    //유니티 에디터 상단 메뉴에 MyMenu 메뉴와 My Window 메뉴 생성.
    [MenuItem("MyMenu/My Window")]
    static void ShowWindow()
    {
        //윈도우 창 표시
        EditorWindow.GetWindow<MyWindow>();
        //윈도우 크기 설정하려면 
        EditorWindow.GetWindowWithRect<MyWindow>(new Rect(10, 10, 250, 250));
        //존재하는 윈도우를 표시하고, 만약 존재하지 않는다면 생성
        //EditorWindow.GetWindow(typeof(MyWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field!", myString);

        /////////////
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optionasl Settings!", groupEnabled); //그룹 시작
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider!", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup(); //그룹 끝
        /////////////
        




        
    }


}
