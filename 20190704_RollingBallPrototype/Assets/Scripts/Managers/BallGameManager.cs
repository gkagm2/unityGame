using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField] public UserInfo user;



    public bool isPlayerFail; // 게임도중 잡혔을 경우.
    public bool gameOver;


    [Header("프레임 제한")]
    public int gameFrame;    

    public enum GameStartState
    {
        newStart,
        continueStart
    };
    
    Coroutine co_revivalTimer; // Timer 제어를 위한 코루틴

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = gameFrame; //프레임 제한 
        InitGameSetting(); // 초기 세팅
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    // 부활 타이머 시작
    public void StartRevivalTimer(int timer)
    {
        co_revivalTimer = StartCoroutine(IRevialTimerOn(timer));
    }
    // 부활 타이머 종료
    public void StopRevivalTimer()
    {
        StopCoroutine(co_revivalTimer);
    }
    // 부활 타이머 코루틴
    IEnumerator IRevialTimerOn(int timer)
    {
        if(timer <= 0)
        {
            timer = 15;
            Debug.LogWarning("Timer가 0보다 커야 합니다. 기본 값 15로 세팅함.");
        }
        float maxTime = (float)timer;
        while(timer > 0)
        {
            // TODO : 이미지가 바뀌지 않는 이유는?
            UIManager.instance.continue_revivalTimerImg.fillAmount = timer / maxTime;
            UIManager.instance.continue_TimerText.text = timer.ToString(); // UI에 뿌려주기
            
            Debug.Log("Timer : " + timer);
            yield return new WaitForSeconds(1);
            --timer; 
        }
        UIManager.instance.ContinuePopup(false); // popup창 종료
    }
    // 게임 일시정지
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
    
    

    // 게임 시작
    public void StartGame(GameStartState state)
    {
        switch (state)
        {
            case GameStartState.newStart:
                SetFirstGameStart();

                break;

            case GameStartState.continueStart:

                break;

        }
        gameOver = false;
        isPlayerFail = false;
        Debug.Log("CLick start");
    }

    // 게임 초기 세팅
    public void InitGameSetting()
    {
        // 게임 세팅
        SetCurrentLevel(1); // level1로 세팅

        user = new UserInfo();
        user.id = "jang";
        user.scoreFromTheGame = 0;
        user.topScore = 0;
        user.coin = 0;
        user.coinFromTheGame = 0;
        user.revivalItem = 0;
        user.protectedItem = 0;
        
        // 플레이어 정보 세팅
        PlayerPrefs.GetInt("topScore", user.topScore);
        PlayerPrefs.GetString("id", user.id);
        PlayerPrefs.GetInt("coin", user.coin);

        // 아이템 세팅
        PlayerPrefs.GetInt("revivalItem", user.revivalItem);
        PlayerPrefs.GetInt("protectedItem", user.protectedItem);

        gameOver = true;
        isPlayerFail = true;
    } 

    // 유저 정보를 저장
    public void SaveUserInfoToDB()
    {
        // 내부 DB에 저장
        PlayerPrefs.SetInt("topScore", user.topScore);
        PlayerPrefs.SetString("id", user.id);
        PlayerPrefs.SetInt("coin", user.coin);

        // 아이템 세팅
        PlayerPrefs.SetInt("revivalItem", user.revivalItem);
        PlayerPrefs.SetInt("protectedItem", user.protectedItem);

        // 외부 DB에 저장
    }

    // 맵 리셋
    public void ResetMap()
    {
        AllDestroyObjectWithTag("Wall");
        AllDestroyObjectWithTag("BoostItem");
        AllDestroyObjectWithTag("Coin");
    }
    

    // 해당 태그의 모든 객체 파괴.
    private void AllDestroyObjectWithTag(string tagName)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
        
        for (int i = 0; i < objs.Length; i++)
        {
            Destroy(objs[i]);
        }
    }

    // 게임이 끝난 후 정보 결과 업데이트
    public void UpdateUserInfoAfterGameOver()
    {
        user.coin += user.coinFromTheGame;
        if(user.topScore < user.scoreFromTheGame)
            user.topScore = user.scoreFromTheGame;
        
        user.scoreFromTheGame = 0;
        user.coinFromTheGame = 0;
    }


    // 게임이 처음으로 시작 할 경우 게임 세팅
    public void SetFirstGameStart()
    {
        isPlayerFail = true;
        gameOver = true;

        user.scoreFromTheGame = 0; // 0으로 초기화
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
