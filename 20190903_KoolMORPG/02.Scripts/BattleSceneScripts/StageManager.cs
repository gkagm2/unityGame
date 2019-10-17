using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] monster;
    public GameObject[] monsterPool;
    public GameObject[] area;
    public Transform[] enemySpawnPoint;
    public Transform playerSpawnPoint;
    public CameraPosition cameraPosition;
    public UserData userData;
    public AudioSource[] audios;

    public int stageLength;
    public int stageIdx = 1;
    public int areaIdx = 1;
    public int monsterCount = 0;

    public bool isClear = false;

    public TextAsset jsonData;
    public JSONNode json;

    [Header("디버그용 텍스트")]
    public Text debugText;
    public Text debugText2;
    public Text debugText3;
    public Text debugText4;
    
    //UI관련
    [Header("UI 관련")]
    public HUDManager hm;
    public GameObject loadingPanel;
    public GameObject inGameUi;
    public GameObject finishPanel;
    public GameObject[] starImages;         // 성공 화면에 띄울 별 이미지
    public Text rewardText;                 // 성공 화면에 띄울 보상 텍스트
    public Text finishPopUpText;
    public string clear = "Stage Clear";
    public string failed = "Stage Failed";


    [System.Serializable]
    public class Monster
    {
        public List<GameObject> monsterPool;

        public Monster()
        {
            monsterPool = new List<GameObject>();
        }
    }

    [Header("")]
    public Monster[] monsters;

    void Awake()
    {
        //PlayerInformation.userData.characterType = EPlayerCharacterType.warrior;
        stageIdx = PlayerInformation.stageData.stageNumber;
        areaIdx = 1;
        //Test용
    }

    void Start()
    {
        JsonInitialIze();
        StartCoroutine(LoadStage());
    }
    //private void LateUpdate() //테스트용 업데이트
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        isClear = true;
    //        StageFinish();
    //    }
    //    debugText.text = "area1 = " + area[0].transform.name.ToString();
    //    debugText2.text = "고블린0번  =" + monsters[0].monsterPool[0].activeSelf;
    //    debugText3.text = "area3 = " + area[2].transform.name.ToString();
    //    debugText4.text = "areaIdx = " + testAreaIdx.ToString();
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isClear = true;
            StageFinish();
        }
    }

    public IEnumerator LoadStage()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Stage0" + stageIdx, LoadSceneMode.Additive);        //비동기 방식으로 현재 씬에 불러올 씬을 병합

        yield return asyncLoad;                                                                                     //불러올 씬이 로드될 때 까지 대기 
    
        if (asyncLoad.isDone)                                                                                       //씬 병합이 정상적으로 이루어 지면 이후 초기화 진행
        {
            GetUserData();
            PlayerInitialize();
            EnemySpawnPointInit();
            MonsterPoolInitialize();
            cameraPosition.FindPlayer();
            GetAudioSource();
            GetStageLengthInfo();
            hm.HUDInitialize();
            loadingPanel.SetActive(false);
            inGameUi.SetActive(true);
        }
    }

    public void GetUserData()
    {
        userData = PlayerInformation.userData.GetInGameCharacterData();
    }

    public void PlayerInitialize()
    {
        playerSpawnPoint = GameObject.Find("PlayerSpawnPoint").GetComponent<Transform>();

        switch (userData.characterType/*PlayerInformation.userData.characterType*/)  //PlayerInformation.userData.characterType Test용
        {
            case EPlayerCharacterType.archer:

                GameObject LoadArcher = Resources.Load<GameObject>("Player/InGameArcher");
                GameObject InstantiateArcher = Instantiate(LoadArcher) as GameObject;
                
                InstantiateArcher.transform.position = playerSpawnPoint.position;
                InstantiateArcher.transform.rotation = playerSpawnPoint.rotation;
                
                break;
            case EPlayerCharacterType.warrior:

                GameObject LoadWarrior = Resources.Load<GameObject>("Player/InGameWarrior");
                GameObject InstantiateWarrior = Instantiate(LoadWarrior) as GameObject;

                InstantiateWarrior.transform.position = playerSpawnPoint.position;
                InstantiateWarrior.transform.rotation = playerSpawnPoint.rotation;

                break;
            default:
                break;
        }
    }

    public void JsonInitialIze()
    {
        json = JSON.Parse(jsonData.text);
    }

    public void MonsterPoolInitialize()
    {
        string stageNum = "Stage" + stageIdx;                                                                       //JsonData의 프로퍼티를 변수로 캐싱

        monsters = new Monster[json[stageNum]["monsterId"].Count];                                                  //몬스터 종류의 배열 길이를 동적 할당
        for (int i = 0; i < monsters.Length; i++)                                                                   //몬스터의 종류 수만큼 for문 실행
        {
            monsters[i] = new Monster();                                                                            //몬스터 리스트 생성
            for (int j = 0; j < (int)json[stageNum]["monsterQuantity"][i]; j++)                                     //JsonData의 각 몬스터 별 필요한 수량 만큼 for문 실행
            {
                GameObject obj = Instantiate(monster[(int)json["Stage" + stageIdx]["monsterId"][i]]) as GameObject; //JsonData의 각 몬스터 Index를 받아와서 해당 Index를 가진 몬스터 생성
                obj.GetComponent<EnemyBasicComponent>().MonsterInitiallize();                                       //몬스터의 상태값 초기화
                obj.name = obj.name + j;                                                                            //몬스터의 이름 지정
                monsters[i].monsterPool.Add(obj);                                                                   //각 몬스터 리스트에 추가
                obj.SetActive(false);                                                                               //생성된 몬스터를 비활성화
            }
        }
    }

    public void EnemySpawnPointInit()
    {
        area = GameObject.FindGameObjectsWithTag("Area");                                   //Area태그 검색으로 오브젝트 검색
       
        GameObject temp;
        for (int i = 0; i < area.Length; i++)                                               // 오브젝트의 이름을 통한 버블정렬 과정
        {
            for (int j = i + 1; j < area.Length; j++)
            {
                if(int.Parse(area[i].transform.name) > int.Parse(area[j].transform.name))
                {
                    temp = area[i];
                    area[i] = area[j];
                    area[j] = temp;
                }
            }
        }
        enemySpawnPoint = area[areaIdx - 1].GetComponentsInChildren<Transform>();          //enemySpawnPoint 초기화
    }

    public void NextArea()
    {
        enemySpawnPoint = area[areaIdx - 1].GetComponentsInChildren<Transform>();         //enemySpawnPoint 초기화
    }

    public void MonsterEnable()
    {
        string stageNum = "Stage" + stageIdx;                                                                                                                                    
        string stageAreaNum = "stageinfo" + areaIdx;

        for (int i = 0; i < json[stageNum][stageAreaNum].Count; i++)                                                                                                           
        {
            GameObject ToEnableMonster = monsters[(int)json[stageNum][stageAreaNum][i]].monsterPool[(int)json[stageNum][stageAreaNum][0]]; //활성화시킬 몬스터 종류의 리스트의 0번
            Monster monsterArray = monsters[(int)json[stageNum][stageAreaNum][i]];                                                         //활성화시킬 몬스터가 들어있는 배열
            ToEnableMonster.transform.position = enemySpawnPoint[i].position;                                                              //활성화시킬 몬스터의 위치를 알맞은 위치로 이동
            ToEnableMonster.SetActive(true);                                                                                               //몬스터 활성화

            monsterCount++;                                                                                                                //몬스터의 수 관리를 위해 카운트를 더해준다.                                                     
                                                
            monsterArray.monsterPool.Remove(ToEnableMonster);                                                                              //몬스터 리스트에서 활성화시킨 몬스터 삭제
            monsterArray.monsterPool.Add(ToEnableMonster);                                                                                 //활성화시킨 몬스터 리스트에 다시 추가              
        }
    }


    public void GetStageLengthInfo()
    {
        stageLength = area.Length;
    }

    public void StageFinish()
    {
        StartCoroutine(IStageFinished());
    }

    public void LoadMainScene()
    {
        SceneMoveManager.LoadScene("MainScene","Map_MainScene");
    }

    public IEnumerator IStageFinished()
    {
        inGameUi.SetActive(false);
        finishPanel.SetActive(true);
        if (isClear)
        {
            PlayerInformation.stageData.eClearState = EClearState.clear;

            finishPopUpText.text = clear;
            int starNumber = (int)(PlayerInformation.stageData.eStageLevel) + 1; // 별 개수
            SetStarCountImage(starNumber); // 별 개수를 설정해서 UI로 보여준다.
            PlayerInformation.stageData.currentStageStarCount = starNumber;
            Debug.Log("성공한 별의 개수 : " + PlayerInformation.stageData.currentStageStarCount);
            // TODO (장현명) : 스테이지의 정보를 저장하는 부분을 작성해야 한다.


            // TODO (장현명) : 보상 타입 나중에 고치기 ()
            ///////////////////////////////// Start ///////////////////////////
            ERewardType rewardType = PlayerInformation.stageData.rewardType; // 스테이지의 보상 타입을 가져옴.
            string rewardMessage = ""; // 보상 메세지
            switch (rewardType)
            {
                case ERewardType.crystal : // 크리스탈 보상
                    rewardMessage = "크리스탈";
                    break;
                case ERewardType.equipmentItem : // 장비 보상
                    rewardMessage = "장비";
                    break;
                case ERewardType.gold : // 골드 보상
                    rewardMessage = PlayerInformation.stageData.StageGold.ToString() + "골드";
                    userData.gold += PlayerInformation.stageData.StageGold;
                    break;
                case ERewardType.stat: // 스텟 보상 ( 이것은 스텟 설정 고쳐야 함)
                    rewardMessage = "스텟";
                    break;
                default:
                    Debug.Assert(false, "보상 타입 범위 초과");
                    break;

            }
            rewardText.text = "보상 : " + rewardMessage;
            ////////////////////////////////// End /////////////////////////////
            
            float rewardExp = PlayerInformation.stageData.StageExp;

            while (rewardExp > 0)
            {
                if (rewardExp >= (userData.MaxExp - userData.exp))
                {
                    rewardExp = rewardExp - (userData.MaxExp - userData.exp);

                    userData.exp = 0;
                    userData.level += 1;
                }
                else
                {
                    userData.exp = userData.exp + rewardExp;
                    rewardExp = 0.0f;
                }
            }
            PlayerInformation.userData.CopyData(userData);
            PlayerInformation.userData.SetCharacterStat(userData.characterType, PlayerInformation.userData.level);
            
            if(NetworkManager.instance!=null)
            {
                yield return NetworkManager.instance.ISaveCharacterDataToServer();
            }
            else
            {
                Debug.LogWarning("네트워크 매니저 없음");
            }
        }
        else
        {
            PlayerInformation.stageData.eClearState = EClearState.fail;
            finishPopUpText.text = failed;
        }
    }

    /// <summary>
    /// 별 이미지의 개수를 설정하여 UI에 업데이트한다.
    /// </summary>
    /// <param name="count">설정 할 별의 개수 (0 ~ 3)</param>
    public void SetStarCountImage(int starCount)
    {
        starCount = Mathf.Clamp(starCount, 0, starImages.Length); // 배열 범위 초과 방지

        for(int i=0; i<starImages.Length; ++i)
        {
            if(starCount - 1 == i)
            {
                starImages[i].SetActive(true);
            }
            else
            {
                starImages[i].SetActive(false);
            }
        }
    }

    public void GetAudioSource()
    {
        audios = FindObjectsOfType<AudioSource>();
        if(PlayerPrefs.GetInt("SoundEffectCheck") == 0)
        {
            for (int i = 0; i < audios.Length; i++)
            {
                audios[i].mute = true;
            }
        }
    }

    public void SoundEffectOn()
    {
        PlayerPrefs.SetInt("SoundEffectCheck", 1);
        Debug.Log(PlayerPrefs.GetInt("SoundEffectCheck") + "효과음 상태 = ");
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].mute = false;
        }
    }

    public void SoundEffectOff()
    {
        PlayerPrefs.SetInt("SoundEffectCheck", 0);
        Debug.Log(PlayerPrefs.GetInt("SoundEffectCheck") + "효과음 상태 = ");
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].mute = true;
        }
    }
}


