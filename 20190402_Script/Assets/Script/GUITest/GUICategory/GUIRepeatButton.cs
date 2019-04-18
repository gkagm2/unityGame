using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIRepeatButton : MonoBehaviour {

    private void OnGUI()
    {
        if(GUI.RepeatButton(new Rect(25, 25, 100, 30), "RepeatButton"))
        {
            print("click");
        }
    }



}
