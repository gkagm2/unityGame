using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUITest : MonoBehaviour {

    private void OnGUI()
    {
        //백그라운드 박스 생성.
        GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu");

        if (GUI.Button(new Rect(20, 40, 80, 20), "Level 1"))
        {
            //사용되지 않음
            //Application.LoadLevel(1);
            SceneManager.LoadScene(1);
        }
        if(GUI.Button (new Rect( 20 ,70 ,80,20), "Level 2"))
        {
            SceneManager.LoadScene(2);
        }
        //Blink Button
        if(Time.time % 2 < 1)
        {
            if(GUI.Button(new Rect(10,10,200,20), "Meet the flashing button"))
            {
                print("You clicked me!");
            }
        }
    }
}
