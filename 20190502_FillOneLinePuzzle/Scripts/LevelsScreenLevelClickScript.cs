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

    void Start()
    {
        SetupLevelState();
    }

    void Update()
    {
        SetupLevelState();
    }

    // 버튼을 눌렸을 경우
    public void OnClickBtn()
    {
        ScreenManager screenManager = GameObject.Find(MyPath.gameScreen).GetComponent<ScreenManager>();
        if (screenManager)
        {
            screenManager.LevelsScreen_LevelBtn(myLevel);

        }
    }


    //레벨 상태를 설정한다.
    public void SetupLevelState()
    {
        PlayerInfo playerInfo = GameObject.Find(MyPath.gameScreen).GetComponent<PlayerInfo>();

        if (myLevel <= playerInfo.myProgressLevel) //현재 진행 레벨 밑으로는
        {
            SetUnLockState(); // 모두 unlock한다
        }
    }
    

    public void SetUnLockState()
    {
        GameObject unLockObj = transform.Find("Score").Find("Unlock").gameObject; // 다음 레벨 오브젝트의 자식인 Unlock 오브젝트를 가져옴
        if (unLockObj) //오브젝트를 가져오는데 성공하면
        {
            unLockObj.SetActive(true); // 풀림 오브젝트를 보이게함
            Debug.Log("Success Unlock 풀림 오브젝트 안보이게 함");
        }
        else
        {
            Debug.Log("Level의 자식 Unlock 오브젝트를 찾지 못했다. Error!");
        }
        GameObject lockObj = transform.Find("Score").Find("Lock").gameObject; // 다음 레벨 오브젝트의 자식인 Lock 오브젝트를 가져옴.
        if (lockObj) //오브젝트를 가져오는데 성공하면
        {
            lockObj.SetActive(false); // 잠금 오브젝트를 안보이게 함
            Debug.Log("Succress 잠금 오브젝트 안보이게함");
        }
    }
}
