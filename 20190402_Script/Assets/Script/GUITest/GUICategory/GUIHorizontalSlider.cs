using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHorizontalSlider : MonoBehaviour {
    private float hSliderValue = 0.0f;
    private void OnGUI()
    {
        hSliderValue = GUI.HorizontalSlider(new Rect(25, 25, 100, 30), hSliderValue, 0.0f, 10.0f);
        print(hSliderValue);
    }
}
