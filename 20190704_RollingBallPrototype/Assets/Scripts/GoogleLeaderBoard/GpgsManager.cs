using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using GooglePlayGames.BasicApi;

public class GpgsManager : MonoBehaviour {

    #region Singleton
    public static GpgsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    // Use this for initialization
    void Start () {
#if UNITY_IOS
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
#elif UNITY_ANDROID
        PlayGamesPlatform.Activate(); //android
#endif
        ConectarGoogle();
	}
	
    public void ConectarGoogle()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (true == success)
                Debug.Log("Login");
            else
                Debug.Log("Login Fail !!");
        });
    }
    // 리더보드 보이기
    public void ShowBoard()
    {
        // Sign In 이 되어있지 않은 상태라면
        // Sign In 후 리더보드 UI 표시 요청할 것
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In 성공
                    // 바로 리더보드 UI 표시 요청
                    Social.ShowLeaderboardUI();
                    return;
                }
                else
                {
                    Debug.Log("리더보드 접속 실패!!");
                    // Sign In 실패 
                    // 그에 따른 처리
                    return;
                }
            });
            
        }
    }
    // 업적 보이기
    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

}
