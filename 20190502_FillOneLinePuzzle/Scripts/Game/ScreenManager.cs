using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    /// <summary>
    /// ///////////////나중에 지우기////////////////////////////////////////////
    /// </summary>
    GameManager gameManager;
    // Main button control
    public Camera camera; //현재 보고있는 화면

    // Screen (Scene)
    

    public GameObject gameScreen;
    short screenNumber;

    
    public  class LevelStageInfo
    {
        string level;
        string stage;
    }
    LevelStageInfo levelStageInfo;
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////
    /// </summary>



    public GameObject mainScreen; // main scene의 Object를 가져옴

    Stack<GameObject> screenStack = new Stack<GameObject>(); // 씬을 저장할 Stack 생성



    // Use this for initialization
    void Start () {

        //////////////////////////////////// 나중에 지우기///////////////////////

        // 게임 매니저 스크립트를 불러옴.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ////////////////////////////////////////////////////////////////////////////

        screenStack.Push(mainScreen); // mainScreen을 push 함. (최초로 켜지는 씬)

        StartCoroutine("Loading");

    }
    IEnumerator Loading()
    {
        Debug.Log("sdfsdf");
        yield return new WaitForSeconds(0.2f);
        GameObject gameScreen = gameObject.transform.Find("GameScreen").gameObject; // gameScreen을 찾음.
        if (gameScreen)
        {
        gameScreen.SetActive(false); // 종료 시킴

        }
        else
        {
            Debug.Log("dsfsdf");
        }

    }

    // Update is called once per frame
    void Update () {
	}

    /// <summary>
    /// MainScreen Buttons
    /// </summary>
    /// 
    // ShareScreen Buttons
    public void GotoShopScreen() //shopScreen으로 가는 버튼
    {
        if(screenStack.Peek().name != "ShopScreen") //현재 씬이 ShopScreen이 아니면
        {
            GameObject shopScreen = gameObject.transform.Find("ShopScreen").gameObject;//shopScreen을 찾아라
            if (shopScreen) // shopScreen이 존재하면
            {
                Debug.Log("ShopScreen이 SetActive true 됨");
                ChangeScreen(shopScreen); //화면에 보여주는 스크린을 바꿈
            }
            else
            {
                Debug.Log("shopScreen을 찾지 못했습니다.");
            }
        }
    }
    public void GotoLevelsScreen() //levelsScreen으로 가는 버튼
    {
        GameObject levelsScreen = gameObject.transform.Find("LevelsScreen").gameObject; // levelsScreen을 찾아라
        if (levelsScreen) // levelsScreen이 존재하면
        {
            Debug.Log("LevelsScreen이 SetActive true됨");
            ChangeScreen(levelsScreen); //화면에 보여주는 스크린을 바꿈
        }
    }
    public void GotoStageScreen() // stageScreen으로 가는 버튼
    {
        GameObject stageScreen = gameObject.transform.Find("StageScreen").gameObject; // stageScreen을 찾아라
        if (stageScreen) // stageScreen이 존재하면
        {
            //TODO : 여기 해야 함
            Debug.Log("stageScreen이 SetActive true됨");
            ChangeScreen(stageScreen); //화면에 보여주는 스크린을 바꿈
        }
    }

    // 레벨 스크린에서 레벨1 버튼을 눌렸을 경우
    public void LevelsScreen_LevelBtn1()
    {
        GameObject levelBtnLock = GameObject.Find("UI Root/Screens/LevelsScreen/Scroll View/Grid/Level1/Score/Lock");// 레벨 버튼의 Lock을 찾는다
        if (levelBtnLock) // 레벨 버튼의 Lock을 찾았으면
        {

            if (levelBtnLock.activeSelf == true) // 잠겨있으면
            {
                return; //실행 중단.
            }
        }
        PlayerInfo playerInfo = GetComponent<PlayerInfo>(); //플레이어 정보를 가져온다
        if (playerInfo) // 가져왔으면
        {
            playerInfo.currentLevel = 1; //현재 선택한 레벨을 설정해줌
            GotoStageScreen(); // stageScreen으로 가는 버튼
            
        }
    }

    // 레벨 스크린에서 레벨2 버튼을 눌렸을 경우
    public void LevelsScreen_LevelBtn2()
    {
        GameObject levelBtnLock = GameObject.Find("UI Root/Screens/LevelsScreen/Scroll View/Grid/Level2/Score/Lock");// 레벨 버튼의 Lock을 찾는다
        if (levelBtnLock) // 레벨 버튼의 Lock을 찾았으면
        {

            if (levelBtnLock.activeSelf == true) // 잠겨있으면
            {
                return; //실행 중단.
            }
        }
        PlayerInfo playerInfo = GetComponent<PlayerInfo>(); //플레이어 정보를 가져온다
        if (playerInfo) // 가져왔으면
        {
            playerInfo.currentLevel = 2; //현재 선택한 레벨을 설정해줌
            GotoStageScreen(); // stageScreen으로 가는 버튼
        }
    }

    // 레벨 스크린에서 레벨3 버튼을 눌렸을 경우
    public void LevelsScreen_LevelBtn3()
    {
        GameObject levelBtnLock = GameObject.Find("UI Root/Screens/LevelsScreen/Scroll View/Grid/Level3/Score/Lock");// 레벨 버튼의 Lock을 찾는다
        if (levelBtnLock) // 레벨 버튼의 Lock을 찾았으면
        {

            if (levelBtnLock.activeSelf == true) // 잠겨있으면
            {
                return; //실행 중단.
            }
        }
        PlayerInfo playerInfo = GetComponent<PlayerInfo>(); //플레이어 정보를 가져온다
        if (playerInfo) // 가져왔으면
        {
            playerInfo.currentLevel = 3; //현재 선택한 레벨을 설정해줌
            GotoStageScreen(); // stageScreen으로 가는 버튼
        }
    }

    // 레벨 스크린에서 레벨4 버튼을 눌렸을 경우
    public void LevelsScreen_LevelBtn4()
    {
        GameObject levelBtnLock = GameObject.Find("UI Root/Screens/LevelsScreen/Scroll View/Grid/Level4/Score/Lock");// 레벨 버튼의 Lock을 찾는다
        if (levelBtnLock) // 레벨 버튼의 Lock을 찾았으면
        {

            if (levelBtnLock.activeSelf == true) // 잠겨있으면
            {
                return; //실행 중단.
            }
        }
        PlayerInfo playerInfo = GetComponent<PlayerInfo>(); //플레이어 정보를 가져온다
        if (playerInfo) // 가져왔으면
        {
            playerInfo.currentLevel = 4; //현재 선택한 레벨을 설정해줌
            GotoStageScreen(); // stageScreen으로 가는 버튼
        }
    }

    // 레벨 스크린에서 레벨5 버튼을 눌렸을 경우
    public void LevelsScreen_LevelBtn5()
    {
        GameObject levelBtnLock = GameObject.Find("UI Root/Screens/LevelsScreen/Scroll View/Grid/Level5/Score/Lock");// 레벨 버튼의 Lock을 찾는다
        if (levelBtnLock) // 레벨 버튼의 Lock을 찾았으면
        {

            if (levelBtnLock.activeSelf == true) // 잠겨있으면
            {
                return; //실행 중단.
            }
        }
        PlayerInfo playerInfo = GetComponent<PlayerInfo>(); //플레이어 정보를 가져온다
        if (playerInfo) // 가져왔으면
        {
            playerInfo.currentLevel = 5; //현재 선택한 레벨을 설정해줌
            GotoStageScreen(); // stageScreen으로 가는 버튼
        }
    }




    void ChangeScreen(GameObject obj) // 화면에 보여주는 스크린을 파라미터 오브젝트(스크린으)로 바꿈
    {
        screenStack.Peek().SetActive(false); // 현재 스크린을 안보이게 하고
        screenStack.Push(obj); // Screen을 Push 함
        screenStack.Peek().SetActive(true); //이후 shopScreen을 보이게 함.
    }

    // ShareScreen Buttons
    public void BackBtn() //뒤로 가는 버튼
    {
        GameObject scene = screenStack.Pop(); //현재 스크린을 Pop하고
        scene.SetActive(false); // 현재 스크린을 안보이게 한다.
        screenStack.Peek().SetActive(true); // 이전 스크린을 보이게 한다.
    }

    // 팝업창을 없앤다.
    public void ClosePopup(GameObject obj)
    {
        obj.SetActive(false);
    }

    // 팝업창을 띄운다.
    public void OpenPopup(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void OpenMenuPopup()
    {
        GameObject menuPopup = gameObject.transform.Find("MenuPopup").gameObject; // menuPopup을 찾아라
        if (menuPopup) // menuPopup이 존재하면
        {
            Debug.Log("menuPopup 이 뜬다.!");
            OpenPopup(menuPopup); // 팝업창을 띄운다.
        }
    }

<<<<<<< HEAD
    // 팝업창을 없앤다.
    public void ClosePopup(GameObject obj)
    {
        obj.SetActive(false);
    }

    // 팝업창을 띄운다.
    public void OpenPopup(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void OpenMenuPopup()
    {
        GameObject menuPopup = gameObject.transform.Find("MenuPopup").gameObject; // menuPopup을 찾아라
        if (menuPopup) // menuPopup이 존재하면
        {
            Debug.Log("menuPopup 이 뜬다.!");
            OpenPopup(menuPopup); // 팝업창을 띄운다.
=======
    /// 메뉴 팝업창의 버튼

    // Menu Popup창을 종료한다.
    public void MenuPopup_OnClickResumeBtn()
    {
        GameObject menuPopup = gameObject.transform.Find("MenuPopup").gameObject; // menuPopup을 찾아라
        if (menuPopup)
        {
            ClosePopup(menuPopup);
        }
    }

    // Menu Popup 창에서 Stage 선택창으로 돌아간다.
    public void MenuPopup_OnClickSelectStageBtn()
    {
        GameObject menuPopup = gameObject.transform.Find("MenuPopup").gameObject; // menuPopup을 찾아라
        if (menuPopup)
        {
            //TODO : 스테이지 선택창.
>>>>>>> 01e911dfa5aca94627c8a9d4b1800470a923721b
        }
    }

    /// 메뉴 팝업창의 버튼

<<<<<<< HEAD
    // Menu Popup창을 종료한다.
    public void MenuPopup_OnClickResumeBtn()
    {
        GameObject menuPopup = gameObject.transform.Find("MenuPopup").gameObject; // menuPopup을 찾아라
        if (menuPopup)
        {
            ClosePopup(menuPopup);
        }
=======
    // Menu Popup 창에서 Main Menu창으로 넘어간다.
    public void MenuPopup_OnClickMainMenu()
    {
        GotoMainMenuScreen(); // 메인메뉴 창으로 간다.
    }


    public void GotoMainMenuScreen()
    {
        //TODO : 스택에 있는 것들을 모두 다 빼고 MainMenu창인 스택만 남기는 것을 구현해야 함.
>>>>>>> 01e911dfa5aca94627c8a9d4b1800470a923721b
    }

    // Menu Popup 창에서 Stage 선택창으로 돌아간다.
    public void MenuPopup_OnClickSelectStageBtn()
    {
        GameObject menuPopup = gameObject.transform.Find("MenuPopup").gameObject; // menuPopup을 찾아라
        if (menuPopup)
        {
            //TODO : 스테이지 선택창.
        }
    }


    // Menu Popup 창에서 Main Menu창으로 넘어간다.
    public void MenuPopup_OnClickMainMenu()
    {
        GotoMainMenuScreen(); // 메인메뉴 창으로 간다.
    }
    
    public void GotoMainMenuScreen()
    {
        //TODO : 스택에 있는 것들을 모두 다 빼고 MainMenu창인 스택만 남기는 것을 구현해야 함.
        
        while(screenStack.Count > 1) //Main창만 남기고 다 Pop한다.
        {
            GameObject obj = screenStack.Pop(); //pop 한뒤
            Debug.Log("Pop한 Obj의 이름 : " + obj.name);
            obj.SetActive(false);  // 모두 setActive false로 바꿈.
        }
    }

    // Success 창에서 Main Menu창으로 넘어간다.
    public void SuccessScreen_OnClickMainMenuBtn()
    {
        GotoMainMenuScreen(); // 메인메뉴 창으로 간다.
    }
    // Success 창에서 Share 버튼을 클릭 시
    public void SuccessScreen_OnClickShareBtn()
    {
        Share();
    }
    // Success 창에서 NextStage 버튼을 클릭 시
    public void SuccessScreen_OnClickNextStageBtn()
    {
        Debug.Log("NextStage button을 클릭했음. (코드작성 해야 함)");
    }

    // 공유 하기 
    public void Share()
    {
        Debug.Log("공유하기~");
    }


    public void ClickTestBtn()
    {
        Debug.Log("잘 찍힌다.");
    }

    //public int GetCurrentLevel()
    //{
    //    string level = "Level";
    //    for (short tlevel = 1; tlevel <= 10; tlevel++) //
    //    {
    //        if (currentScreen.name == level + tlevel.ToString())
    //        {
    //            return tlevel; //level을 알려줌

    //        }
    //    }
    //    return 0; //0이면 못찾은 것.
    //}


    //public void OnClickBackbtn()
    //{
    //    currentScreen.SetActive(false);
    //    mainScreen.SetActive(true);
    //}

    //Main Button Control
    //public void MainOnClickLevelsBtn()
    //{
    //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;


    //    for (int i = 0; i < levelScreen.Length; i++)
    //    {
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider.name.Equals("LevelBtn" + (i+1)))
    //            {

    //                mainScreen.SetActive(false);
    //                currentScreen = levelScreen[i];
    //                levelScreen[i].SetActive(true);
    //                gameManager.currentPlayLevel = GetCurrentLevel(); //현재 레벨을 가져와 설정
    //                Debug.Log("현재 선택한 레벨 : " + gameManager.currentPlayLevel);
    //                if(gameManager.currentPlayLevel == 0)
    //                {
    //                    Debug.Log("현재 Level을 담지 못했습니다.");
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}

    //// Level 스크린에서 Stage버튼을 눌렀을 경우.
    //public void LevelOnClickStagesBtn()
    //{
    //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit)) //레이캐스트를 쏴서 충돌시킴.
    //    {
    //        Debug.Log(hit.collider.name);
    //        for(int i=0; i< 100; i++) //모든 스테이지를 검색한다. 
    //        {

    //            if (hit.collider.name.Equals("Stage (" + i + ")")) //스테이지의 이름과 같으면
    //            {
    //                levelScreen[screenNumber].SetActive(false); //현재 레벨 스크린을 사라지게 하고
    //                currentScreen = gameScreen;
    //                gameScreen.SetActive(true); //게임 스크린을 킨다. 


    //                break;
    //            }
    //        }

    //    }
    //}

}
