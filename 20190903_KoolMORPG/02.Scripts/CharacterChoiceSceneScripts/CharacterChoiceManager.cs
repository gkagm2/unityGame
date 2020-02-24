using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 캐릭터 선택 창을 관리하는 클래스
/// </summary>
public class CharacterChoiceManager : MonoBehaviour
{
    public Button leftButton;                           // 왼쪽 버튼
    public Button rightButton;                          // 오른쪽 버튼
    public CameraRotator cameraRotator;                 // CameraRotator Object의 CameraRotator component

    [Tooltip("현재 선택한 캐릭터")]
    public EPlayerCharacterType currentCharacterType = EPlayerCharacterType.none;   // 현재 선택되어지고 있는 캐릭터

    [Header("캐릭터 설명 UI Panel")]
    public Text characterTypeText;                      // 캐릭터 타입 텍스트
    public Text characterDescriptionText;               // 캐릭터 설명 텍스트
    public Image characterImage;                        // 캐릭터 이미지

    [Header("캐릭터 능력치 UI Panel")]
    public Slider hpSlider;                             // 체력 슬라이더
    public Slider attackSlider;                         // 공격력 슬라이더
    public Slider defenceSlider;                        // 방어력 슬라이더
    public Slider attackSpeedSlider;                    // 공격 속도 슬라이더

    [Header("버튼")]
    public GameObject createCharacterButton;            // 캐릭터 생성 버튼
    public GameObject characterChoiceButton;            // 캐릭터 선택 버튼
    public GameObject backButton;                       // MainScene으로 돌아가는 버튼

    private int characterIndex;                         // characters의 Index를 가리킬 변수
    private UserData[] defaultCharacterData;            // 0 : Warrior, 1 : Archar의 default
    private readonly int characterCount = 2;            // 캐릭터 개수

    private readonly float defaulMaxHp = 200f;          // 기본으로 설정되어있는 최대 HP
    private readonly float defaultMaxAtk = 40f;         // 기본으로 설정되어있는 최대 공격력
    private readonly float defaultMaxDef = 40f;         // 기본으로 설정되어있는 최대 방어력
    private readonly float defaultMaxAttackSpeed = 2f;  // 기본으로 설정되어있는 최대 공격 속도

    [Header("LoadingPanel")]
    public GameObject loadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        loadingPanel.SetActive(true);
        InitSetting();
    }

    /// <summary>
    /// MainScene이 켜지면 처음 세팅을 해준다.
    /// </summary>
    public void InitSetting()
    {
        defaultCharacterData = new UserData[characterCount];
        Debug.Log("defaultCharacterIndex : " + characterIndex);
        
        // 캐릭터의 상태가 아무것도 없으면// 현재 캐릭타입 상태가 none일 경우 현재 타입을 Warrior로 변환한다.
        if (EPlayerCharacterType.none == PlayerInformation.userData.characterType)
        { 
            currentCharacterType = EPlayerCharacterType.warrior;
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }

        // 현재 캐릭터 상태가 archar일 경우 archar가 있는 곳으로 화면을 돌린다
        if (EPlayerCharacterType.archer == PlayerInformation.userData.characterType)
        {
            currentCharacterType = EPlayerCharacterType.archer;
            OnClick_RightCharacterChoiceBtn();
        }
        Debug.Log("chararcterIndex 333 : " + characterIndex);
        StartCoroutine(IInitSetting());
    }
    private IEnumerator IInitSetting()
    {
        if (NetworkManager.instance == null)
        {
            Debug.LogWarning("NetworkManager가 없습니다. 유저의 캐릭터가 존재하는지 체크 할 수 없습니다. LoadingScene에서 시작하십시오");
        }
        else
        {
            // 유저의 캐릭터가 존재하는지 체크한다. (PlayerInformation.isExistCharacter에서 반환 값을 얻을 수 있다.)
            for (int i = 0; i < characterCount; i++)
            {
                yield return StartCoroutine(NetworkManager.instance.ICheckCharacterExist(PlayerInformation.userData.name, (EPlayerCharacterType)(i + 1)));
            }
        }
        UpdateUIScreen();
    }

    /// <summary>
    /// 왼쪽 캐릭터 선택 버튼 클릭. 
    /// </summary>
    public void OnClick_LeftCharacterChoiceBtn()
    {
        --characterIndex;
        if (leftButton.IsInteractable())
        {
            cameraRotator.RotateLeft();
        }
        UpdateUIScreen(); // 화면을 새롭게 업데이트
    }

    /// <summary>
    /// 오른쪽 캐릭터 선택 버튼 클릭
    /// </summary>
    public void OnClick_RightCharacterChoiceBtn()
    {
        ++characterIndex;
        if (rightButton.IsInteractable())
        {
            cameraRotator.RotateRight();
        }
        UpdateUIScreen(); // 화면을 새롭게 업데이트
    }

    /// <summary>
    /// 새로운 캐릭터 생성 버튼을 누를 경우 실행
    /// </summary>
    public void OnClick_CreateNewCharacterBtn()
    {
        StartCoroutine(ICreateNewCharacterBtn());
    }
    private IEnumerator ICreateNewCharacterBtn()
    {
        // 다른 캐릭터가 있는 상태에서 새로운 유저를 생성하는 경우에
        if(PlayerInformation.userData.characterType != EPlayerCharacterType.none)
        {
            PlayerInformation.inventory.InitUserInventory(); // 기존에 있던 인벤토리 정보들을 삭제한다.
        }
        PlayerInformation.userData.characterType = currentCharacterType;

        if (NetworkManager.instance == null)
        {
            Debug.LogWarning("NetworkManager가 없습니다. 새로운 유저의 정보를 서버에 생성 할 수 없습니다.");
        }
        else
        {
            // 새로운 캐릭터 유저인가?
            bool isExistUser = false;
            
            if (NetworkManager.instance.isExistCharacter[(int)currentCharacterType - 1] == true)
            {
                isExistUser = true;
            }
            if (!isExistUser)
            {
                // 새로운 유저의 정보를 서버에 생성하게 한다.
                yield return StartCoroutine(NetworkManager.instance.ICreateNewUserInformationToServer(PlayerInformation.eUserCreateMode));

                // 서버에 모든 스테이지의 별의 개수를 0으로 초기화한다.
                for (int stageNumber = 1; stageNumber <= PlayerInformation.stageData.maxStageCount; ++stageNumber) {
                    yield return StartCoroutine(NetworkManager.instance.ISetStageInformationToServer(stageNumber, 0));
                }
            }
        }
        
        PlayerInformation.userData.SetGoods(1000,50); // 재화를 설정한다.
        NetworkManager.instance.CreateNewUserCharacterDataToServer(); // PlayerInformation.userData의 값을 서버로 보낸다. 
        PlayerPrefs.SetString("Name", PlayerInformation.userData.name);
        SceneMoveManager.LoadScene("MainScene", "Map_MainScene");
    }

    /// <summary>
    /// 캐릭터 선택 버튼을 누를 경우 실행
    /// </summary>
    public void OnClick_SelectCharacterBtn()
    {
        StartCoroutine(ISelectCharacterBtn());
    }

    private IEnumerator ISelectCharacterBtn()
    {
        // 새로운 유저가 아닐경우 
        PlayerInformation.userData.characterType = currentCharacterType;

        if (NetworkManager.instance != null)
        {
            PlayerInformation.inventory.InitUserInventory(); // 기존에 있던 인벤토리 정보들을 삭제한다.

            // 선택한 캐릭터의 정보를 서버로부터 불러와 PlayerInformation.userData에 넣어준다.
            yield return StartCoroutine(NetworkManager.instance.ILoadCharacterDataFromServer(PlayerInformation.userData.name, currentCharacterType));

            // 선택한 캐릭터의 장비 정보를 서버로부터 불러와 PlayerInformation.inventory.equipmentItemDataList에 넣어준다.
            yield return StartCoroutine(NetworkManager.instance.ILoadEquipmentItemsFromServer());

            // 선택한 캐릭터의 소비 아이템들의 정보를 서버로부터 불러와 PlayerInformation.inventory.consumableItemDataList에 넣어준다.
            yield return StartCoroutine(NetworkManager.instance.ILoadConsumableItemsFromServer());

            // 장착한 아이템들의 정보를 서버로부터 가져온다.
            yield return StartCoroutine(NetworkManager.instance.ILoadEquippedItemsFromServer());

            // 캐릭터의 스테이지 정보들을 가져온다.
            yield return StartCoroutine(NetworkManager.instance.IGetStagesInformation());
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없음");
        }
        PlayerPrefs.SetString("Name", PlayerInformation.userData.name);
        SceneMoveManager.LoadScene("MainScene", "Map_MainScene");
    }

    /// <summary>
    /// MainScene으로 돌아간다.
    /// </summary>
    public void OnClick_BackBtn()
    {
        SceneMoveManager.LoadScene("MainScene", "Map_MainScene");
    }

    /// <summary>
    /// 캐릭터 선택 창의 모든 UI를 새롭게 업데이트 한다
    /// </summary>
    private void UpdateUIScreen()
    {
        UpdateButtonImage(); // 버튼 이미지 업데이트
        currentCharacterType = (EPlayerCharacterType)characterIndex + 1;
        UpdateDefaultCharacterInformation(); // 캐릭터의 기본 정보로 업데이트
        UpdateCharacterDescription(currentCharacterType); // 캐릭터 설명 창 업데이트
        
        Debug.Log("characterIndex : " + characterIndex + " current character Type : " + currentCharacterType);
        // 캐릭터 타입이 이미 설정 되어 있으면

        Debug.Log("현재 플레이어 상태 :  " + PlayerInformation.userData.characterType + ",픽창 캐릭 상태 : " + currentCharacterType); ;
    }

    /// <summary>
    /// 캐릭터의 설명 창을 업데이트 한다.
    /// </summary>
    /// <param name="characterType">선택 할 캐릭터 타입</param>
    private void UpdateCharacterDescription(EPlayerCharacterType characterType)
    {
        CharacterData characterData = null;
        // 캐릭터 설명 창 업데이트
        if (characterType == EPlayerCharacterType.warrior)
        {
            characterData = Resources.Load<CharacterData>(PathOfResources.warriorData);
            characterImage.sprite = Resources.Load<Sprite>(PathOfResources.warriorFaceImage);
        }
        else if(characterType == EPlayerCharacterType.archer)
        {
            characterData = Resources.Load<CharacterData>(PathOfResources.archarData);
            characterImage.sprite = Resources.Load<Sprite>(PathOfResources.archarFaceImage);
        }
        else
        {
            characterTypeText.text = "";
            characterDescriptionText.text = "";
        }

        characterTypeText.text = characterData.characterType;
        characterDescriptionText.text = characterData.characterDescription;

        PlayerInformation.userData.ChangeFaceImage(characterType); // 캐릭터 얼굴 이미지 바꾸기

        // 캐릭터 스텟 창 UI 업데이트
        //Debug.Log("hp : " + PlayerInformation.userData.hp + ", atk: " + PlayerInformation.userData.atk + ", def : " + PlayerInformation.userData.def + ", attack speed : " + PlayerInformation.userData.attackSpeed);
        hpSlider.value = PlayerInformation.userData.hp / defaulMaxHp;
        attackSlider.value = PlayerInformation.userData.atk / defaultMaxAtk;
        defenceSlider.value = PlayerInformation.userData.def / defaultMaxDef;
        attackSpeedSlider.value = PlayerInformation.userData.attackSpeed / defaultMaxAttackSpeed;
    }

    /// <summary>
    /// 캐릭터의 기본 정보로 업데이트
    /// </summary>
    private void UpdateDefaultCharacterInformation()
    {
        // 선택한 캐릭터의 기본 스텟을 설정한다. (PlayerInformation.userData에 설정 됨)
        PlayerInformation.userData.SetCharacterStat(currentCharacterType);
    }

    /// <summary>
    /// 버튼 이미지를 업데이트한다.
    /// </summary>
    private void UpdateButtonImage()
    {
        // 왼쪽, 오른쪽 버튼
        if (characterIndex <= 0) // 0은 none이므로 사용하지 않는다.
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        if (characterIndex >= defaultCharacterData.Length - 1)
        {
            rightButton.interactable = false;
            leftButton.interactable = true;
        }

        Debug.Log("------>:" + (EPlayerCharacterType)(characterIndex + 1));
        // 캐릭터 타입이 이미 설정 되어 있으면 버튼을 unable 상태로 만든다.
        if (PlayerInformation.userData.characterType == (EPlayerCharacterType)(characterIndex + 1))
        {
            createCharacterButton.GetComponent<Button>().interactable = false;
            characterChoiceButton.GetComponent<Button>().interactable= false;
            SetButtonProperty();
        }
        else // 캐릭터 타입이 이미 설정이 안되어있으면
        {
            createCharacterButton.GetComponent<Button>().interactable = true;
            characterChoiceButton.GetComponent<Button>().interactable = true;
            SetButtonProperty();
        }
    }

    /// <summary>
    /// 선택 혹은 생성 버튼으로 설정한다. 
    /// </summary>
    private void SetButtonProperty()
    {
        if (NetworkManager.instance != null)
        {
            // 선택 버튼
            if (NetworkManager.instance.isExistCharacter[characterIndex]) // 캐릭터가 존재하면
            {
                createCharacterButton.SetActive(false);
                characterChoiceButton.SetActive(true);
            }
            else
            {
                createCharacterButton.SetActive(true);
                characterChoiceButton.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("NetworkManager가 없음.");
        }
    }
}
