using UnityEngine;
using UnityEngine.Monetization;
public class AdManager : MonoBehaviour {

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

    private const string store_id = "3222259"; // store (Android)
    private string video_ad = "video";
    private string rewarded_video_ad = "rewardedVideo";
    private string banner_ad = "GetRevivalItem";
    // Use this for initialization
    void Start() {
        Monetization.Initialize(store_id, true); //gameId, testMode
    }

    // Update is called once per frame
    void Update() {
    }

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


}