using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void StartGame()
    {
        // warning : 레벨 1에 해당하는 씬 이름을 넣어야 함.
        SceneManager.LoadScene("CH1_Awakening");
    }
}
