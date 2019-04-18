using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILayout03 : MonoBehaviour {
    //그룹 안에 또 다른 그룹을 품기
    public Texture2D bgImage;
    public Texture2D fgImage;

    public float playerEnergy = 1.0f;
    
    void OnGUI()
    {
        //두 이미지를 가진 하나의 그룹 생성
        //첫 두 좌표는 스크린에서 그룹을 시작할 좌표.
        GUI.BeginGroup(new Rect(0, 0, 256, 32));

        //배경 이미지를 그리고,
        GUI.Box(new Rect(0, 0, 256, 32), bgImage);

        //그룹 안에 그룹을 생성
        //사용자가 원하는 대로 스케일이 변하는 그룹을 생성
        GUI.BeginGroup(new Rect(0, 0, playerEnergy * 256, 32));

        //이미지를 그리고,
        GUI.Box(new Rect(0, 0, 256, 32), fgImage);

        //그룹을 종료
        GUI.EndGroup();
        GUI.EndGroup();
    }
}
