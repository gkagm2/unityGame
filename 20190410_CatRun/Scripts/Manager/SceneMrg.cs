using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMrg : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void GotoGameSceneBtn(short sceneNumber)
    {
        Debug.Log("gameScene");
        SceneManager.LoadScene(sceneNumber);
    }
    public void GotoGameSceneBtn(string sceneName)
    {
        Debug.Log("gameScene");
        SceneManager.LoadScene(sceneName);
    }

    public void GameExitBtn()
    {
        Debug.Log("Game 종료");
        Application.Quit();
    }
}
