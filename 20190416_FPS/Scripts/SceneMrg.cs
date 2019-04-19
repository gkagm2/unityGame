using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMrg : MonoBehaviour {

    public GameObject keepgoingObj;

	// Update is called once per frame
	void Update () {
		
	}

    public void GotoTitleScene()
    {
        if(PauseScript.gamePause == true)
        {
            PauseScript.SetPause(false);
        }
        SceneManager.LoadScene("Title");
    }

    public void GotoInGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void GotoResultScene()
    {
        SceneManager.LoadScene("Result");
    }
    public void GameExitBtn() {
        Application.Quit();
    }
    
    public void KeepGoingBtn()
    {
        keepgoingObj.SetActive(false);
    }
}
