using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class UnityAdsHelper : MonoBehaviour {
    const string androidGameID = "3152813";
    const string IosGameID = "3152812";

    const string rewardedVideoID = "rewardedVideo";
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Advertisement.Show(); // 광고 호출.
    }
    private void Initialize()
    {

    }
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
            Debug.Log("보상을 받는다.");
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The as was successfully shown.");
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown");
                break;
        }
    }
}

