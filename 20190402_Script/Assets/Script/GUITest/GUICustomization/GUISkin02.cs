using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISkin02 : MonoBehaviour {
    public GUISkin mySkin;
    private bool toggle = true;
    
    //OnGUI()함수에서 여러 GUISkin 적용
    private void OnGUI()
    {
        //이제부터 사용될 GUI에 Skin 적용
        GUI.skin = mySkin;

        //토글 버튼 생성 앞에서 적용한 GUI Skin에 영향을 받는다.
        toggle = GUI.Toggle(new Rect (10,10,150,20), toggle, "Skinned Button", "button");

        //적용된 GUI Skin 제거
        GUI.skin = null;

        //기본 스타일의 버튼 생성
        GUI.Button(new Rect(10, 35, 140, 20), "Built-in Button");
    }
}
