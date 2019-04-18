using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIToggle : MonoBehaviour {
    private bool toggleBool = true;

    private void OnGUI()
    {
        toggleBool = GUI.Toggle(new Rect(25, 25, 100, 30), toggleBool, "Toggle");
    }

}
