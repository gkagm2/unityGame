using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIVerticalSlider : MonoBehaviour {
    private float vSliderValue = 0.0f;

    private void OnGUI()
    {
        vSliderValue = GUI.VerticalSlider(new Rect(25, 25, 100, 30), vSliderValue, 10.0f, 0.0f);
    }



}
