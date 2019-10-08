using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main scene의 상단 UI를 보여주는 클래스
/// </summary>
public class TopPopup : MonoBehaviour
{
    [Header("Top popup")]
    public Text stamina;                    // 행동력 텍스트
    public Text goldText;                   // 골드 텍스트
    public Text crystalText;                // 크리스탈 텍스트

    private void Start()
    {
        UpdateUI();
    }
    
    /// <summary>
    ///  Top popup의 UI를 업데이트 한다.
    /// </summary>
    public void UpdateUI()
    {
        // Tpop opopup UI
        // TODO (장현명) : 최대 스테미나, 골드, 크리스탈의 개수를 만들어야 한다.
        stamina.text = PlayerInformation.userData.Stamina.ToString() + "/" + PlayerInformation.userData.MaxStamina.ToString();
        goldText.text = PlayerInformation.userData.gold.ToString();
        crystalText.text = PlayerInformation.userData.crystal.ToString();
    }
}
