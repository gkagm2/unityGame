using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	public static bool gamePause = false;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
	}
	public void PauseBtn(){
		gamePause = !gamePause;
		
		if(gamePause == true){
			Time.timeScale = 0;

		}else{
			Time.timeScale = 1;
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
