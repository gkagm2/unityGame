using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameManager : MonoBehaviour {

    #region Singleton
    public static BallGameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public bool isPlayerFail; // 게임도중 잡혔을 경우.
    public bool gameOver;


    [Header("프레임 제한")]
    public int gameFrame;



    

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = gameFrame; //프레임 제한 

        SetFirstGameStart();
	}
	
	// Update is called once per frame
	void Update () {

	}


    public void PauseGame()
    {

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Debug.Log("high");
            Time.timeScale = 1;
        }
    }
    


    public void StartGame()
    {
        gameOver = false;
        isPlayerFail = false;
        Debug.Log("CLick start");
    }


    // 게임이 처음으로 시작 할 경우 게임 세팅
    public void SetFirstGameStart()
    {
        isPlayerFail = true;
        gameOver = true;
    }


    // 게임 실패했을 경우.
    public void FailGame()
    {
        isPlayerFail = true;
        UIManager.instance.ContinuePopup(true); // 이어서 하기 팝업 띄우기.
    }



    // 현재 레벨 세팅
    public void SetCurrentLevel(int level)
    {
        //Level.currentLevel = currentLevel;
        // 한줄이면 되는데.. Button에 OnClick 이벤트에서 enum은 못 받는듯.. 코드가 길어진다.
        
        switch (level)
        {
            case 1:
                Level.currentLevel = LevelState.level1;
                break;
            case 2:
                Level.currentLevel = LevelState.level2;
                break;
            case 3:
                Level.currentLevel = LevelState.level3;
                break;
            case 4:
                Level.currentLevel = LevelState.level4;
                break;
            case 5:
                Level.currentLevel = LevelState.level5;
                break;
            case 6:
                Level.currentLevel = LevelState.level6;
                break;
            case 7:
                Level.currentLevel = LevelState.level7;
                break;
            default:
                Debug.Log("해당 레벨 없음");
                break;
        }
        Debug.Log("현재 레벨을 " + level + "로 변경함.");
    }
}
