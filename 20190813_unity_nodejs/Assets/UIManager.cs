using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    public static UIManager instance;  // singleton

    public Text goldText;
    public Text scoreText;
    public Text dateText;
    public Text userNameText;

    #region Singleton
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    // UI의 모든 정보를 업데이트
    public void UpdateUI()
    {
        goldText.text = UserManager.gold.ToString();
        scoreText.text = UserManager.score.ToString();
    }



    public void OnClick_GoldPlusBtn()
    {
        UserManager.gold += 10;
        goldText.text = UserManager.gold.ToString();
    }

    public void OnClick_GoldMinusBtn()
    {
        if (UserManager.gold <= 0 || UserManager.gold - 10 <= 0 )
            UserManager.gold = 0;
        else
            UserManager.gold -= 10;


    goldText.text = UserManager.gold.ToString();
    }

    public void OnClick_ScorePlusBtn()
    {
        UserManager.score += 100;
        scoreText.text = UserManager.score.ToString();
    }

    public void OnClick_ScoreMinusBtn()
    {
        if (UserManager.score <= 0 || UserManager.score - 100 <= 0)
            UserManager.score = 0;
        else
            UserManager.score -= 100;
        scoreText.text = UserManager.score.ToString();
    }

    public void OnClick_UpdateDataBtn()
    {
        NetworkManager.instance.UpdateDataToServer();
    }

    public void OnClick_PauseBtn()
    {
    }
}
    