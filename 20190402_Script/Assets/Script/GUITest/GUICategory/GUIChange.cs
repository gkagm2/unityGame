using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIChange : MonoBehaviour {

    private int selectedToolbar = 0;
    private string[] toolbarStrings = { "One", "Two" };

    private void OnGUI()
    {
        //어떤 버튼이 클릭되었는지 확인
        selectedToolbar = GUI.Toolbar(new Rect(50, 10, Screen.width - 100, 30), selectedToolbar, toolbarStrings);

        //만약에 툴바에 사용자 입력이 있으면 다음 함수는 true
        if (GUI.changed)
        {
            Debug.Log("The toolbar was clicked");
            if (0 == selectedToolbar)
                Debug.Log("First button was clicked");
            else
                Debug.Log("Second button was clicked");
        }
    }

}
