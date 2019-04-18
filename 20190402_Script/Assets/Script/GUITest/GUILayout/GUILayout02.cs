using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILayout02 : MonoBehaviour {

    void OnGUI()
    {
        //화면 중앙에 그룹 생성
        GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));
        //이제부터 나오는 GUI는 그룹 안에 그려지게 되며,
        //그리는 영역도 그룹의 왼쪽 위롤 0,0으로 기준으로 잡음

        //그룹 안에 나올 GUI들을 생성
        GUI.Box(new Rect(0, 0, 100, 100), "Group is here");
        GUI.Button(new Rect(10, 40, 80, 30), "Click me");

        //그룹의 범위는 EndGroup까지, BeginGroup과 EndGroup은 꼭 쌍으로 존재.
        GUI.EndGroup();
        
    }
}
