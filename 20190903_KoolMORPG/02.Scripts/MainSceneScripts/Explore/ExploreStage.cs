using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;
/// <summary>
/// 플레이어가 게임에 들어가기 앞서 선택해야되는 스테이지의 정보를 담고있는 클래스
/// </summary>
public class ExploreStage : MonoBehaviour
{
    public ExploreStageManager exploreStageManager;     
    public Text stageNumberText;                                // 스테이지의 별의 개수 Text
    public GameObject[] starsImageObject;                       // 별들의 이미지 오브젝트들을 가져온다.
    public int stageNumber = 0;                                 // 스테이지 번호
    public bool isClear = false;

    private int currentStarCount = 0;                           // 현재 별의 개수
    private readonly int maxStarCount = 3;                      // 최대 별의 개수

    void Start()
    {
    }
    
    /// <summary>
    /// UI이미지를 업데이트한다.
    /// </summary>
    public void UpdateUI()
    {
        ShowStarImages();   // 별의 이미지를 보여준다.

        stageNumberText.text = stageNumber.ToString();
        Debug.Log(gameObject.name + "Stage number :" + stageNumber);
        Debug.Log("starCount : " + currentStarCount + "로 이미지 출력");
    }

    /// <summary>
    /// 별의 이미지를 보여준다.
    /// </summary>
    public void ShowStarImages()
    {
        for (int i = 0; i <= maxStarCount - 1; i++)
        {
            if (i == currentStarCount - 1)
            {
                starsImageObject[i].SetActive(true);
            }
            else
            {
                starsImageObject[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// 스테이지 버튼을 클릭 할 경우 실행되는 함수
    /// </summary>
    public void OnClick_SelectStageBtn()
    {
        exploreStageManager.selectedStageNumber = stageNumber; // 스테이지 번호를 저장
        Debug.Log(gameObject.name + "카드의 stageNumber : " +stageNumber + "exploreStageManager.SN : " + stageNumber);
        exploreStageManager.OpenStageReadyPopup(true);
    }

    /// <summary>
    /// 별의 개수를 설정한다.
    /// </summary>
    /// <param name="count">설정할 별의 개수(범위 : 0~3)</param>
    public void SetStarCount(int count)
    {
        count = Mathf.Clamp(count, 0, maxStarCount);
        currentStarCount = count;

        if(currentStarCount > 0)
        {
            isClear = true;
        }
    }

    /// <summary>
    /// 게임 내부에서 스테이지 상태 저장하기.
    /// </summary>
    /// <param name="starCount">현재 별의 개수</param>
    public void SaveStageStateInApplication()
    {
        //JSONObject stageInfoJson = new JSONObject();
        //// save
        //stageInfoJson.Add(PlayerInformation.stageData.stageFileName + stageNumber, currentStarCount);

        //// json파일을 저장할 디렉토리가 없으면 생성한다.
        //DirectoryInfo diPath = new DirectoryInfo(PlayerInformation.stageData.stageInformationSavePath);
        //if (diPath.Exists == false)
        //{
        //    diPath.Create();
        //}

        //if (File.Exists(PlayerInformation.stageData.stageInformationSavePath + PlayerInformation.stageData.stageFileName + stageNumber))
        //{
        //    Debug.Log("파일이 존재한다.");
        //    File.WriteAllText(PlayerInformation.stageData.stageInformationSavePath + PlayerInformation.stageData.stageFileName + stageNumber, stageInfoJson.ToString());
        //}
        //else
        //{
        //    File.Create(PlayerInformation.stageData.stageInformationSavePath + PlayerInformation.stageData.stageFileName + stageNumber);
        //    Debug.Log("파일 생성");
        //}
    }

    /// <summary>
    /// 게임 내부에서 스테이지 상태 불러오기.
    /// </summary>
    public void LoadStageStateInApplication()
    {
        //    string path = PlayerInformation.stageData.stageInformationSavePath;

        //    if (File.Exists(path))
        //    {
        //        UpdateUI();
        //    }
        //    else
        //    {
        //        Debug.Log("파일이 존재하지 않음 : " + stageNumber);
        //        File.Create(PlayerInformation.stageData.stageInformationSavePath + PlayerInformation.stageData.stageFileName + stageNumber);
        //    }
        //}
    }
}
