using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
/// <summary>
/// 스테이지의 정보를 담은 클래스
/// </summary>
public class StageData
{
    public int stageNumber = 0;          // 스테이지 번호 (default = 0) 
    public EStageLevel eStageLevel;      // 스테이지 레벨
    public int StageExp
    {
        get
        {
            return ((int)eStageLevel + 1) * 5;
        }
    }              // 스테이지 클리어 시 얻는 경험치 (Exp : Experience)

    public EClearState eClearState = EClearState.none; // 클리어 상태

    public int StageGold                 // 스테이지 클리어 시 얻는 골드
    {
        get
        {
            int gold = 0;
            // TODO FIXED (장현명) : 나중에 확장을 고려하기위해 공식으로 만들어야 함.
            switch (eStageLevel)
            {
                case EStageLevel.easy:
                    switch (stageNumber)
                    {
                        case 1:
                            gold = 1000;
                            break;
                        case 2:
                            gold = 2000;
                            break;
                        case 3:
                            gold = 3000;
                            break;
                        case 4:
                            gold = 4000;
                            break;
                        case 5:
                            gold = 5000;
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case EStageLevel.normal:
                    switch (stageNumber)
                    {
                        case 1:
                            gold = 6000;
                            break;
                        case 2:
                            gold = 7000;
                            break;
                        case 3:
                            gold = 8000;
                            break;
                        case 4:
                            gold = 9000;
                            break;
                        case 5:
                            gold = 10000;
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                    break;
                case EStageLevel.difficult:
                    switch (stageNumber)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            gold = 10000;
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    Debug.Assert(false, "오류발생 스테이지 레벨 범위 벗어남");
                    break;
            }
            return gold;
        }
    }

    // TODO (장현명) : 공식화 하던가 아니면 임의로 넣어주던가 해야 할 듯.
    public int currentStageStarCount;           // 게임 클리어 시 스테이지의 별 개수 범위는 (1 ~ 3)
    public ERewardType rewardType;// 게임 클리어 시 보상 타입.

    public string stageFileName {
        get {
            return "StageInfo" + PlayerInformation.userData.characterType.ToString() + PlayerInformation.userData.name;
        } 
    }

    //public readonly string stageInformationSavePath = Application.persistentDataPath + "/Resources/StageInfo/";
    // TODO (장현명) : 최대 스테이지의 개수를 자동으로 가져올 수 있게 만들기.
    public int maxStageCount = 5;        // 최대 스테이지의 개수
    public int[] stagesStarCount;             // 각 스테이지의 별의 개수를 담을 배열

    /// <summary>
    /// 초기화 함수
    /// </summary>
    public StageData()
    {
        stagesStarCount = new int[maxStageCount]; // 스테이지마다의 별의 개수를 담을 배열을 업데이트한다.
    }

    /// <summary>
    /// 스테이지 데이터 클래스의 멤버변수 정보를 초기화한다.
    /// </summary>
    public void InitData()
    {
        stageNumber = 0;
        eStageLevel = EStageLevel.normal;
        eClearState = EClearState.none;
        currentStageStarCount = 0;
        rewardType = ERewardType.gold;
    }
}