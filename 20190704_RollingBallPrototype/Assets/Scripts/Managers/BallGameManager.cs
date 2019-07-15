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

    
<<<<<<< HEAD
=======

>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5

    // 게임 시작 상태
    public enum GameStartStatus
    {
        newStart,
        continueStart
    };
    


    Coroutine co_RevivalTimer; // Timer 제어를 위한 코루틴
    Coroutine co_IncreaseScore; // 게임 시작시 오름.

    public int revivalTimer = 30; // 부활 타이머

    public int increasingScore = 1; // 게임에서 더하는 점수.
<<<<<<< HEAD

    private int coinValue = 10; // 코인 하나의 가치(가격)
=======
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5


    // Use this for initialization
    void Start () {
        Application.targetFrameRate = gameFrame; //프레임 제한 
        InitGameSetting(); // 초기 세팅
        
	}
	
	// Update is called once per frame
	void Update () {
        

    }

<<<<<<< HEAD
    // TODO : ( 201907015 상점에서 구입하는 것은 따로 함수를 만드는 경우도 고려하고있다. 생각해보고 이상하면 바꾸던가 하겠다.)
    // 코인을 얻는다. 
    public void GetCoin(WhereGetCoinStatus status)
    {
        switch(status){
            case WhereGetCoinStatus.InTheGame: // 게임에서 얻었을 경우.
                user.coinFromTheGame += coinValue;
                UIManager.instance.UpdateGameUI_InGamePlayScreen(GameEventOccurStatus.GetCoin); // Coin UI만 업데이트

                Debug.Log("user 코인 :" + user.coinFromTheGame);

                break;
            case WhereGetCoinStatus.InTheStore: // 상점에서 얻었을 경우.
                // TODO : 상점에서 얻을 경우 작성.
                break;
        }
    }

    // 점수 올라가기 활성화
    public void ActivateRaiseUpScore(ActivateStatus activateStatus)
    {
        if (activateStatus == ActivateStatus.Start)
=======
    // 점수 올라가기 활성화
    public void ActivateRaiseUpScore(ActivateState activateState)
    {
        if (activateState == ActivateState.Start)
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
            co_IncreaseScore = StartCoroutine(IIncreaseScore());
        else
            StopCoroutine(co_IncreaseScore);

    }
    // 점수 올리기 코루틴
    IEnumerator IIncreaseScore()
    {
        while (true)
        {
            user.scoreFromTheGame += increasingScore;
            UIManager.instance.gamePlay_scoreText.text = user.scoreFromTheGame.ToString();
            yield return new WaitForSeconds(0.2f);
        }
    }



    // 부활 타이머 활성화
<<<<<<< HEAD
    public void ActivateRevivalTimer(ActivateStatus activateStatus, int timer = 10)
    {
        if(activateStatus == ActivateStatus.Start) //시작 시 코루틴 시작
=======
    public void ActivateRevivalTimer(ActivateState activateState, int timer = 10)
    {
        if(activateState == ActivateState.Start) //시작 시 코루틴 시작
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
            co_RevivalTimer = StartCoroutine(IRevialTimerOn(timer));
        else // 타이머 멈춤
            StopCoroutine(co_RevivalTimer);
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
    public void StartGame(GameStartStatus status)
    {
        switch (status)
        {
            case GameStartStatus.newStart:
                SetFirstGameStart();

                break;

<<<<<<< HEAD
            case GameStartStatus.continueStart:
                user.revivalItem -= user.numNeededForRevivalItem; //아이템 깎음.
                ActivateRaiseUpScore(ActivateStatus.Start); // 점수 올라감
=======
            case GameStartState.continueStart:
                user.revivalItem -= user.numNeededForRevivalItem; //아이템 깎음.
                ActivateRaiseUpScore(ActivateState.Start); // 점수 올라감
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
                break;

        }
        gameOver = false;
        isPlayerFail = false;
        Debug.Log("CLick start");
    }

    // 게임이 처음으로 시작 할 경우 게임 세팅
    public void SetFirstGameStart()
    {
        isPlayerFail = true;
        gameOver = true;
<<<<<<< HEAD
        ActivateRaiseUpScore(ActivateStatus.Start); // 점수 올라가기 활성화 됨
        user.scoreFromTheGame = 0; // 0으로 초기화
        user.coinFromTheGame = 0;
        UIManager.instance.UpdateGameUI_InGamePlayScreen(GameEventOccurStatus.UpdateUI); // 게임 화면 업데이트
=======
        ActivateRaiseUpScore(ActivateState.Start); // 점수 올라가기 활성화 됨
        user.scoreFromTheGame = 0; // 0으로 초기화
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
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




    // 게임 실패했을 경우.
    public void FailGame()
    {
        isPlayerFail = true;
<<<<<<< HEAD
        ActivateRaiseUpScore(ActivateStatus.Stop);
=======
        ActivateRaiseUpScore(ActivateState.Stop);
>>>>>>> 59503538d7d101cd2babf6c84eba28dbf1c4b6f5
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
                Level.currentLevel = LevelStatus.level1;
                break;
            case 2:
                Level.currentLevel = LevelStatus.level2;
                break;
            case 3:
                Level.currentLevel = LevelStatus.level3;
                break;
            case 4:
                Level.currentLevel = LevelStatus.level4;
                break;
            case 5:
                Level.currentLevel = LevelStatus.level5;
                break;
            case 6:
                Level.currentLevel = LevelStatus.level6;
                break;
            case 7:
                Level.currentLevel = LevelStatus.level7;
                break;
            default:
                Debug.Log("해당 레벨 없음");
                break;
        }
        Debug.Log("현재 레벨을 " + level + "로 변경함.");
    }

}
