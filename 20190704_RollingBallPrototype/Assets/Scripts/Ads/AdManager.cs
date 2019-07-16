using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;
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
    private const string store_id = "3222258"; // store (Apple)
    private const string gameId = "3222258"; // store (Apple)

#elif UNITY_ANDROID
    private const string store_id = "3222259"; // store (Android)
    private const string gameId = "3222259"; // store (Android)
#endif

    private string video_ad = "video";
    private string rewarded_video_ad = "rewardedVideo";
    private string banner_ad = "GetRevivalItem";



    public string placementId = "rewardedVideo";
    private Button adButton;

    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Finished Ad");
            // Reward the player
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }



    
    /// ///////////////////////////////////////////////





    public void ShowVideoInterstitialAD()
    {
        // is the video ad ready to be played
        if (Monetization.IsReady(video_ad))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(video_ad) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();
            }
        }
    }

    public void PlayRevivalVideo()
    {
        // is the video ad ready to be played
        if (Monetization.IsReady(rewarded_video_ad))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(rewarded_video_ad) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();
            }
        }

    }
    public void BannerVideo()
    {
        // is the video ad ready to be played
        if (Monetization.IsReady(banner_ad))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(banner_ad) as ShowAdPlacementContent;

            if (ad != null)
            {
                ad.Show();

            }
        }
    }

    //public int HandleShowResult(ShowResult result)
    //{
    //    if (result == ShowResult.Finished)
    //    {
    //        Debug.Log("finish reward");
    //        return 1;
    //        // Reward the player
    //    }
    //    else if (result == ShowResult.Skipped)
    //    {
    //        Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
    //        return 0;
    //    }
    //    else if (result == ShowResult.Failed)
    //    {
    //        Debug.LogError("Video failed to show");
    //        return -1;
    //    }
    //    return 2;
    //}


}