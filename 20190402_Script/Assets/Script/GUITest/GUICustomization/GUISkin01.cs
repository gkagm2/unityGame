using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISkin01 : MonoBehaviour {
    public GUISkin mySkin;

    private void OnGUI()
    {
        //이제부터 사용될 GUI에 Skin 적용
        GUI.skin = mySkin;

        //적용된 스킨에 있는 "button" 스타일에 영향을 받는 버튼
        GUI.Button(new Rect(10, 10, 150, 20), "Skinned Button");
    }


}
