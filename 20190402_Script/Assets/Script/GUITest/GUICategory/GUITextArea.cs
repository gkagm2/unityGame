using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITextArea : MonoBehaviour {
    private string textAreaString = "text area";
    private void OnGUI()
    {
        textAreaString = GUI.TextArea(new Rect(25, 25, 100, 30), textAreaString);
    }


}
