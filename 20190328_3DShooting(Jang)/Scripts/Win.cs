using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100.0f, Screen.height / 2 - 100.0f, 200.0f, 200.0f), "You Win!~"))
        {
            //Load "ShootingGame" Scene
            SceneManager.LoadScene(0);
        }
    }
}
