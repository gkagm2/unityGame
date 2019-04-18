using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIButton : MonoBehaviour {

    private void OnGUI()
    {
        if (GUI.Button(new Rect(25, 25, 100, 30), "Button"))
        {
            //버튼이 클릭되면 true
        }
    }


}
