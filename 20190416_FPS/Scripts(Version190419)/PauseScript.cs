using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	public GameObject pauseText;
	public static bool gamePause = false;

	// Use this for initialization
	void Start () {
        Debug.Log("Pause : " + gamePause); 
    }

    // Update is called once per frame
    void Update () {
        Debug.Log("Pause : " + gamePause);
	}
	public void PauseBtn(){
		gamePause = !gamePause;
		
		if(gamePause == true){
			Time.timeScale = 0;
            pauseText.SetActive(true);

		}else{
			Time.timeScale = 1;
            pauseText.SetActive(false);
		}
	}
    public static void SetPause(bool flag)
    {
        if(flag == true)
        {
            gamePause = true;
            Time.timeScale = 0;
        }
        else
        {
            gamePause = false;
            Time.timeScale = 1;
        }

    }
}
