using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

    #region Singleton

    public static AdManager instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    #endregion

#if UNITY_IOS
    private string store_id = SecureInfo.store_id;
    private string gameId = SecureInfo.gameId;

#elif UNITY_ANDROID

    private string store_id = SecureInfo.store_id;
    private string gameId = SecureInfo.gameId;
#endif

    private string video_ad = "video";
    private string rewarded_video_ad = "rewardedVideo";
    private string banner_ad = "GetRevivalItem";
    

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(rewarded_video_ad, new ShowOptions() {resultCallback = HandleAdResult});
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Adver - Finished");
                BallGameManager.instance.StartGame(BallGameManager.GameStartStatus.continueStart); // 이어서 게임 시작
                break; 
            case ShowResult.Skipped:
                Debug.Log("Adver - Skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("Adver - Failed");
                break;
        }
    }
}