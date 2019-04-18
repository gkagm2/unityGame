using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIStyle02 : MonoBehaviour {
    // public으로 GUIStyle 변수를 선언하면 스타일의 모든 요소가 인스펙터 뷰에 표시된다. Inspector에서 설정가능
    public GUIStyle customButton;

    private void OnGUI()
    {
        
        //GUIStyle을 적용한 버튼
        GUI.Button(new Rect(10, 10, 150, 20), "I am a Custom Button", customButton);
    }
}
