using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour {
    public static bool isGameOver = false; //게임 오버인가?

    UIManager uiManager;

	// Use this for initialization
	void Start () {
        uiManager = GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isGameOver) //게임 종료가 아니면
        {

        }
        else
        {
            GameOperationKey(); //게임 조작 키 사용
        }
	}
    public void GameOver()
    {
        //Debug.Log("GameOver");
        if (isGameOver)
            isGameOver = false;
        else
        {
            isGameOver = true;
        }
    }

    public void GameOperationKey()
    {
        if (Input.GetKey(KeyCode.Escape)){
            uiManager.GotoScene("Main");

        }
        if (Input.GetKey(KeyCode.R))
        {
            isGameOver = false;
            uiManager.GotoScene("Game");
        }




    }
}
