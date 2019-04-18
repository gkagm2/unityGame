using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScrollView : MonoBehaviour {
    public Vector2 scrollViewVector = Vector2.zero;
    public string innerText = "I am inside the ScrollView";
    private void OnGUI()
    {
        //스크롤 뷰 시작
        scrollViewVector = GUI.BeginScrollView(new Rect (25, 25, 100, 100), scrollViewVector, new Rect(0, 0, 400, 400));

        //스크롤 뷰 안에 GUI 삽입
        innerText = GUI.TextArea(new Rect(0, 0, 400, 400), innerText);

        //스크롤 뷰 끝
        GUI.EndScrollView();
    }



}
