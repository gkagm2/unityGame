using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool isGameOver; //게임 오버인가?

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!isGameOver) //게임 종료가 아니면
        {
            GameOperationKey(); //게임 조작 키 사용

        }
        else
        {
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
        //Input.GetKey
    }
}
