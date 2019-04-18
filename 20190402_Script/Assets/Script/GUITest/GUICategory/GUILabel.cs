using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILabel : MonoBehaviour {

    private void OnGUI()
    {
        GUI.Label(new Rect(25, 25, 100, 30), "Label");
    }


}
