using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
public class ExploreStageManager : MonoBehaviour
{
    [Tooltip("스테이지 버튼들")]
    public ExploreStage[] exploreStages;                    // 탐험 스테이지 버튼들에 들어가있는 컴포넌트

    public int selectedStageNumber = 0;                     // 선택 된 스테이지 번호
    public EStageLevel eStageLevel;                         // 현재 스테이지 난이도
    public int StaminaCountForStage
    {
        get
        {
            return ((int)eStageLevel + 1) * 5; // 필요한 행동력 수
        }
    }                      // 스테이지를 돌 때 필요한 행동력 수

    [Header("Stage map panel")]
    public GameObject levelsPanel;                          // 레벨 선택 버튼들이 있는 패널
    public GameObject[] levelImageObjects;                  // 0 : easy, 1 : normal, 2 : difficult
    public Text currentLevelText;                           // 현재 레벨을 나타내는 Text

    [Header("Stage ready popup")]
    public GameObject stageReadyPopup;
    public MainScenePopupManager mainScenePopupManager;
    public Text stageLevelText;
    public Text stageNumberText;                            
    public Text neededStaminaCountText;                  // 스테이지를 돌 때 필요한 행동력 수 Text

    //string savePath = PlayerInformation.stageData.stageInformationSavePath;
    //string stageFileName = PlayerInformation.stageData.stageFileName;
    void Start()
    {
        InitUI();
    }

    /// <summary>
    /// UI 상태를 초기화한다.
    /// </summary>
    public void InitUI()
    {
        levelsPanel.SetActive(false); // 레벨 선택 패널을 끈다.
        eStageLevel = EStageLevel.normal;
        UpdateUI();
    }

    /// <summary>
    /// 상태 그대로 유지하며 UI를 업데이트한다.
    /// </summary>
    public void  UpdateUI()
    {
        // 스테이지의 정보를 업데이트한다.
        if(PlayerInformation.stageData.eClearState != EClearState.none) // 무엇인가 상태가 있으면
        {
            PlayerInformation.stageData.InitData();   // 스테이지 데이터 초기화
        }

        // 스테이지의 정보를 업데이트하고 UI를 보여준다.
        for (int i = 0; i < PlayerInformation.stageData.stagesStarCount.Length; i++)
        {
            exploreStages[i].SetStarCount(PlayerInformation.stageData.stagesStarCount[i]); // 각 스테이지의 별의 개수를 설정한다.
            exploreStages[i].UpdateUI(); // 스테이지 버튼들의 UI를 업데이트 한다.
        }

        // 난이도 이미지를 바꾼다.
        for (int i = 0; i < levelImageObjects.Length; i++)
        {
            if ((int)eStageLevel == i)
            {
                Debug.Log("eStageLevel : " + eStageLevel + " i: " + i);
                levelImageObjects[i].SetActive(true);
            }
            else
            {
                levelImageObjects[i].SetActive(false);
            }
        }

        // 난이도 텍스트를 업데이트한다.
        currentLevelText.text = GetLevelTextFromStageLevelOfEnum(eStageLevel);
    }
    
    /// <summary>
    /// enum형태의 난이도를 입력받아 한글 텍스트로 변환해서 리턴해주는 함수.
    /// </summary>
    /// <param name="level">난이도 값</param>
    /// <returns>한글로 변환된 난이도 텍스트</returns>
    public string GetLevelTextFromStageLevelOfEnum(EStageLevel level)
    {
        string levelText = "";
        switch (eStageLevel)
        {
            case EStageLevel.easy:
                levelText = "쉬움";
                break;
            case EStageLevel.normal:
                levelText = "보통";
                break;
            case EStageLevel.difficult:
                levelText = "어려움";
                break;
            default:
                Debug.Assert(false);
                break;
        }
        return levelText;
    }

    //////// Stage Ready Popup ////////

    /// <summary>
    /// 스테이지 준비 팝업창 On/Off 함수
    /// </summary>
    /// <param name="isOpen">창을 띄울지 말지 결정한다.</param>
    public void OpenStageReadyPopup(bool isOpen)
    {
        if (isOpen)
        {
            UpdateUiOfStageReadyPopup();
            stageReadyPopup.SetActive(true);
        }
        else
        {
            stageReadyPopup.SetActive(false);
        }
    }

    /// <summary>
    /// Stage ready popup창에 UI를 업데이트한다.
    /// </summary>
    private void UpdateUiOfStageReadyPopup()
    {
        stageLevelText.text = GetLevelTextFromStageLevelOfEnum(eStageLevel);
        Debug.Log("selectedStageNumber : " + selectedStageNumber);
        stageNumberText.text = selectedStageNumber.ToString();
        neededStaminaCountText.text = "x" + StaminaCountForStage.ToString();
    }

    /// <summary>
    /// 탐험 시작 버튼 클릭 시 실행되는 함수
    /// </summary>
    public void OnClick_ExploreStartBtn()
    {
        if (PlayerInformation.userData.Stamina < StaminaCountForStage)
        {
            PopupManager.instance.OpenAlarmPopup("행동물약이 부족합니다.");
            return;
        }
        
        PlayerInformation.userData.Stamina -= StaminaCountForStage;

        // 필요한 스테이지에 대한 정보를 저장한다.
        PlayerInformation.stageData.stageNumber = selectedStageNumber;
        PlayerInformation.stageData.eStageLevel = eStageLevel;

        // TODO (장현명) : 스테이지 보상 타입을 여러개로 바꾸기
        PlayerInformation.stageData.rewardType = ERewardType.gold;

        if (NetworkManager.instance != null)
        {
            StartCoroutine(IConsumeStamina());
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없어서 서버로 업데이트 된 정보를 보낼 수 없습니다.");
            SceneMoveManager.LoadScene("BattleScene");
        }
    }

    /// <summary>
    /// 행동력을 소모한다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator IConsumeStamina()
    {
        yield return StartCoroutine(NetworkManager.instance.ISaveCharacterDataToServer());
        Debug.Log("행동력 소모 후 서버에 데이터 저장 완료");
        SceneMoveManager.LoadScene("BattleScene");
    }

    ///////// Stage Map Panel ////////

    /// <summary>
    /// 스테이지의 레벨 선택 버튼을 눌렀을 때 실행되는 함수
    /// </summary>
    public void OnClick_OnOffLevelChoiceListBtn()
    {
        if (levelsPanel.activeSelf) // 보이는 상태면 안보이게 한다.
        {
            levelsPanel.SetActive(false);
        }
        else // 보이지 않는 상태면 보이게 한다.
        {
            levelsPanel.SetActive(true);
        }   
    }
    
    /// <summary>
    /// 스테이지의 레벨을 설정한다.
    /// </summary>
    /// <param name="level">설정 할 스테이지 레벨</param>
    public void OnClick_SetStageLevelBtn(int level)
    {
        int enumCount = System.Enum.GetNames(typeof(EStageLevel)).Length; // Enum의 개수를 리턴한다. (Enum.GetNames이 Enum.GetValues보다 빠르다)
        Debug.Log("level enum count : " +enumCount);

        // 레벨 선택 범위를 벗어났을 경우.
        if (level < 0 || level >= enumCount)
        {
            Debug.LogWarning("스테이지의 레벨을 선택할 수 있는 범위를 벗어났습니다. (" + level + "~" + exploreStages.Length + ")");
            return;
        }
        
        eStageLevel = (EStageLevel)level;
        
        UpdateUI(); 
        levelsPanel.SetActive(false);
    }


    ////////////// TOP POPUP //////////
    
    /// <summary>
    /// Main화면으로 가는 Back 버튼 클릭시 실행한다.
    /// </summary>
    public void OnClick_BackToMain()
    {
        gameObject.SetActive(false); // ExploreStagePanel을 끈다.
        mainScenePopupManager.OnClick_AdventureBtn(false);
    }

    /// <summary>
    /// 모든 스테이지들이 클리어 되었는지 확인하여 bool 값을 반환한다.
    /// </summary>
    /// <returns>클리어 했는지 여부</returns>
    public bool IsAllStagesClear()
    {
        for (int i = 0; i < exploreStages.Length; ++i)
        {
            if(!exploreStages[i].isClear) // 클리어 되지 않으면
            {
                Debug.Log("스테이지가 클리어되지 않음");
                return false; //false 리턴
            }
        }
        Debug.Log("모든 스테이지가 클리어 됨.");
        return true; // 모두 클리어 시  true 리턴
    }

    private void OnEnable()
    {
        StartCoroutine(IGetStagesInformation());
    }
    IEnumerator IGetStagesInformation()
    {
        if(NetworkManager.instance != null)
        {
            yield return StartCoroutine(NetworkManager.instance.IGetStagesInformation());
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없습니다.");
        }
        InitUI();
    }
}
