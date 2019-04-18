using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIStyle01 : MonoBehaviour {

    private void OnGUI()
    {
        //box 스타일을 적용한 레이블
        GUI.Label(new Rect(0, 0, 200, 100), "Hi - I'm label looking like a box", "box");

        //버튼을 toggle스타일로 표현
        GUI.Button(new Rect(10, 140, 180, 20), "This is a button", "toggle");
    }



}
