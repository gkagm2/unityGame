using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIToolbar : MonoBehaviour {

    private int toolbarInt = 0;
    private string[] toolbarStrings = { "Toolbar1", "Toolbar2", "Toolbar3" };

    private void OnGUI()
    {
        toolbarInt = GUI.Toolbar(new Rect(25, 25, 250, 30), toolbarInt, toolbarStrings);
        print(toolbarInt);
    }

}
