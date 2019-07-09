using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameManager : MonoBehaviour {
    [Header("Current Level")]
    public LevelState currentLevel;

    public bool isPlayerCatched;
    public bool gameOver;
	// Use this for initialization
	void Start () {
        FirstGameStartSetting();
	}
	
	// Update is called once per frame
	void Update () {

	}


    // 게임이 처음으로 시작 할 경우 게임 세팅
    public void FirstGameStartSetting()
    {
        isPlayerCatched = false;
        gameOver = false;
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
