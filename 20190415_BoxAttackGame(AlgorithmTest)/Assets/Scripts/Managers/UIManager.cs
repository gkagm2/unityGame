using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    //Main Scene//
    public void StartBtn()
    {
        GotoScene("Game");
        GameManager.isGameOver = false;
        SceneManager.LoadScene("Game");
    }
    public void EndBtn()
    {
        Application.Quit();
    }
    //Main Scene End//

    //Game Scene//
    public void GotoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    //Game Scene End//
}
