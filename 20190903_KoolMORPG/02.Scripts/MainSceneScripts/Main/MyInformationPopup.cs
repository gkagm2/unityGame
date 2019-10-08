using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main 화면에 플레이어의 레벨, 이름, 경험치를 UI로 보여주는 클래스
/// </summary>
public class MyInformationPopup : MonoBehaviour
{
    public Text levelText;      // 플레이어의 레벨
    public Text nameText;       // 플레이어의 이름
    public Text expText;        // 플레이어의 경험치

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        levelText.text = PlayerInformation.userData.level.ToString();
        nameText.text = PlayerInformation.userData.name;
        expText.text = PlayerInformation.userData.exp.ToString() + "/" + PlayerInformation.userData.MaxExp.ToString();
    }
}