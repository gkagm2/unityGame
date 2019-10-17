using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdventurePopupManager : MonoBehaviour
{
    [Header("Explore Panel")]
    public GameObject exploreStagePanel;
    public GameObject exploreStageBuzzer;

    /// <summary>
    /// 게임 선택 팝업의 UI를 업데이트한다.
    /// </summary>
    public void UpdateUI()
    {
        // 모두 클리어 시 부저를 바꿈
        if (exploreStagePanel.GetComponent<ExploreStageManager>().IsAllStagesClear())
        {
            exploreStageBuzzer.SetActive(true);
        }
    }

    ////////////////////// Popup ///////////////////

    /// <summary>
    /// 탐험 버튼을 누를 때 호출
    /// </summary>
    public void OnClick_ExploreBtn()
    {
        exploreStagePanel.SetActive(true);
        exploreStagePanel.GetComponent<ExploreStageManager>().InitUI();
    }

    /// <summary>
    /// RPG 버튼을 누를 때 호출 
    /// </summary>
    public void OnClick_MoRpgBtn()
    {
        SceneMoveManager.LoadScene("MORPGScene");
    }

    /// <summary>
    /// 몬스터 헌터 버튼을 누를 때 호출
    /// </summary>
    public void OnClick_MonsterHunterBtn()
    {
        SceneMoveManager.LoadScene("MonsterHunterScene");
    }
}
