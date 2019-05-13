using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class LevelsScreenLevelClickScript : MonoBehaviour {

    short myLevel;
    private void Awake()
    {
        
        myLevel = short.Parse(Regex.Replace(gameObject.name, @"\D", "")); //레벨의 이름에서 숫자만 추출한다.
    }

    // 버튼을 눌렸을 경우
    public void OnClickBtn()
    {
        ScreenManager screenManager = GameObject.Find(Path.gameScreen).GetComponent<ScreenManager>();
        if (screenManager)
        {
            screenManager.LevelsScreen_LevelBtn(myLevel);

        }
    }

}
