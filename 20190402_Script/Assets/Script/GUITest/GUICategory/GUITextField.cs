using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITextField : MonoBehaviour {
    private string textFieldString = "text Field";
    private void OnGUI()
    {
        textFieldString = GUI.TextField(new Rect(25, 25, 100, 30), textFieldString);
        Debug.Log(textFieldString);
    }

    
}
