using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITest2 : MonoBehaviour {
    public Texture2D controlTextrue;
    public Texture2D icon;

    private void OnGUI()
    {
        //  Position 
        GUI.Box(new Rect(0, 0, 100, 50), "Top-left");
        GUI.Box(new Rect(Screen.width - 100, 0, 100, 50), "Top-right");
        GUI.Box(new Rect(0, Screen.height - 50, 100, 50), "Bottom-left");
        GUI.Box(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Bottom-right");

        //  Content 

        //문자열 출력
        GUI.Label(new Rect(0, 110, 100, 50), "Text String Label");

        //이미지 출력
        GUI.Label(new Rect(0, 240, 150, 150), controlTextrue);

        //이미지와 문자열 출력
        if(GUI.Button (new Rect(200, 0, 100, 50), icon))
        {
            print("you clicked the icon");
        }
        if (GUI.Button(new Rect(200, 60, 100, 50), "This is text"))
        {
            print("you clicked the text button");
        }

        //---------------------
        //GUIContent 구조체로 이미지와 문자열을 동시에 출력

        GUI.Button(new Rect(200, 120, 100, 20), new GUIContent("AA button", "TThis is the tooltip"));
        
        //GUI.tooltip이 어떻게 표현될지를 지정
        GUI.Label(new Rect(200,150,100,40), GUI.tooltip);
        //----------------------



    }
}
