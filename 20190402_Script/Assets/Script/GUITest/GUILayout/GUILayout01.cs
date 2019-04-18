using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILayout01 : MonoBehaviour {


private void OnGUI()
    {
        // 고정 레이아웃
        GUI.Button(new Rect(25, 25, 100, 30), "I am a Fixed Layout Button");

        // 자동 레이아웃
        GUILayout.Button("I am an Automatic Layout Button");
    }



}
