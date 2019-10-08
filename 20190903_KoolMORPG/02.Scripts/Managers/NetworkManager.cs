using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;
using System;

/// <summary>
/// 서버와 통신할 때 사용하는 클래스
/// </summary>
public class NetworkManager : MonoBehaviour
{
    #region Singleton
    public static NetworkManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    #endregion

    [HideInInspector]
    public bool isConnected = false;
    public bool isGoogleLoginMode = false; // 구글 아이디로 로그인하여 시작할 경우 true, 구글 로그인 없이 게임 시작 시 false
    public bool isNewUser = false; // 서버쪽에 저장이 안된 새 유저인지?
    public bool[] isExistCharacter;                          // 캐릭터가 존재하는지 여부 (0 : warrior, 1 : archar)


    public EUserCreateMode eUserCreateMode;

    public readonly string CONNECTION_CHECK_URL = "https://kool-player-crud.run.goorm.io/ConnectionCheck";
    public readonly string CHECK_IF_USER_NAME_EXISTS_URL = "https://kool-player-crud.run.goorm.io/checkUserNameExist";
    public readonly string LOAD_CHARACTER_DATA = "https://kool-player-crud.run.goorm.io/getCharacterData";
    public readonly string CREATE_NEW_USER_URL = "https://kool-player-crud.run.goorm.io/createNewUser";
    public readonly string CREATE_NEW_USER_CHARACTER_URL = "https://kool-player-crud.run.goorm.io/addNewCharacterData";
    public readonly string UPDATE_CHARACTER_DATA_URL = "https://kool-player-crud.run.goorm.io/updateCharacterData";
    public readonly string LOAD_EQUIPMENTITEMS_URL = "https://kool-player-crud.run.goorm.io/getEquipmentItemsOfUser";
    public readonly string SAVE_EQUIPMENTITEM_URL = "https://kool-player-crud.run.goorm.io/saveEquipmentItemOfUser";
    public readonly string DELETE_EQUIPMENTITEM_URL = "https://kool-player-crud.run.goorm.io/deleteEquipmentItemOfUser";
    public readonly string CHECK_IF_USER_CHARACTER_EXIST_URL = "https://kool-player-crud.run.goorm.io/checkUserCharacterExist";
    public readonly string ADD_CONSUMABLE_ITEM_URL = "https://kool-player-crud.run.goorm.io/addConsumableItemsOfUser";
    public readonly string LOAD_CONSUMABLE_ITEMS_URL = "https://kool-player-crud.run.goorm.io/getConsumableItemsOfUser";
    public readonly string LOAD_EQUIPPED_ITEMS_URL = "https://kool-player-crud.run.goorm.io/LoadCurrentEquippedItems";
    public readonly string SAVE_EQUIPPED_ITEM_URL = "https://kool-player-crud.run.goorm.io/insertCurrentEquippedItem";
    //public readonly string CHECK_EQUIPPED_ITEM_URL = "https://kool-player-crud.run.goorm.io/checkCurrentEquippedItem";
    public readonly string DELETE_EQUIPPED_ITEM_URL = "https://kool-player-crud.run.goorm.io/deleteEquippedItem";
    public readonly string DELETE_CONSUMABLE_ITEM_URL = "https://kool-player-crud.run.goorm.io/deleteConsumableItemOfUser";
    public readonly string SET_STAGE_NUMBER_URL = "https://kool-player-crud.run.goorm.io/SetExploreStage";
    public readonly string GET_STAGES_INFORMATION_URL = "https://kool-player-crud.run.goorm.io/loadExploreStagesInformation";


    private const string CONNECTION_CHECK_MESSAGE = "connect";
    private const string RESPONSE_CONNECTION_FAIL_MESSAGE = "fail";
    private const string RESPONSE_SUCCESS_MESSAGE = "success";
    private const string RESPONSE_USER_NAME_EXISTS_MESSAGE = "1";


    private void Start()
    {
        isExistCharacter = new bool[Enum.GetNames(typeof(EPlayerCharacterType)).Length - 1];
        for (int i = 0; i < isExistCharacter.Length; ++i)
        {
            Debug.Log("초기화 i : " + i);
            isExistCharacter[i] = false;
        } // false로 초기화
    }

    /// <summary>
    /// 서버와 연결을 시도한다.
    /// </summary>
    /// <returns>응답 메세지</returns>
    public IEnumerator ICheckConnectionToServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("CheckMessage", CONNECTION_CHECK_MESSAGE);

        UnityWebRequest www = UnityWebRequest.Post(CONNECTION_CHECK_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            if (www.downloadHandler.text == RESPONSE_SUCCESS_MESSAGE)
            {
                Debug.Log("연결 성공 : " + www.downloadHandler.text);
                isConnected = true;
            }
            else
            {
                Debug.Log("연결 실패" + www.downloadHandler.text);
                isConnected = false;
            }
        }
    }

    /// <summary>
    /// 유저 이름이 존재하는지 체크한다.
    /// </summary>
    /// <param name="userName">체크 할 유저 이름</param>
    /// <returns>응답 메세지</returns>
    public IEnumerator ICheckUserNameExist(string userName)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", userName);

        UnityWebRequest www = UnityWebRequest.Post(CHECK_IF_USER_NAME_EXISTS_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            string responseMessage = www.downloadHandler.text;
            Debug.Log("응답 메시지를 받음 : " + www.downloadHandler.text);

            //JSONArray json = (JSONArray)JSON.Parse(www.downloadHandler.text);

            if (responseMessage == RESPONSE_USER_NAME_EXISTS_MESSAGE)
            {
                Debug.Log("유저 이름이 존재합니다." + responseMessage);
                PopupManager.instance.OpenAlarmPopup("유저 이름이 존재합니다.");
            }
            else
            {
                PlayerInformation.userData.name = userName;
                isNewUser = true; // 새 유저로 표시

                Debug.Log("유저 이름이 존재하지 않습니다." + responseMessage);
                Debug.Log("userName : " + userName);
                Debug.Log("PlayerInformation.isNew -> 새로운 유져인가?: " + isNewUser);
                SceneMoveManager.LoadScene("CharacterChoiceScene");
            }
        }
    }

    /// <summary>
    /// 서버에 새로운 유저 정보를 생성한다.
    /// </summary>
    /// <param name="userCreateMode">생성할 때의 유저 모드</param>
    /// <returns>응답 메시지</returns>
    public IEnumerator ICreateNewUserInformationToServer(EUserCreateMode userCreateMode)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        if (userCreateMode == EUserCreateMode.withGPGSId) //GPGSID로 로그인하면 
        {
            form.AddField("User_Id", PlayerInformation.userData.id); // GPGS id값도 넣어준다.
        }
        Debug.Log("EUSerCnreateMode : " + userCreateMode);
        Debug.Log("Name : " + PlayerInformation.userData.name);
        if (PlayerInformation.userData.id != null)
        {
            Debug.Log("User_Id : " + PlayerInformation.userData.id);
        }
        UnityWebRequest www = UnityWebRequest.Post(CREATE_NEW_USER_URL, form);
        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("새로운 정보 저장 메시지 : " + www.downloadHandler.text);
            if (www.downloadHandler.text == RESPONSE_SUCCESS_MESSAGE)
            {
                if (userCreateMode == EUserCreateMode.withGPGSId)
                {
                    Debug.Log("(User_ID 포함) 새로운 유저 정보를 서버에 저장하는데 성공했다.");
                }
                else
                {
                    Debug.Log("(Name만 포함) 새로운 유저 정보를 서버에 저장하는데 성공했다.");
                }
            }
            else
            {
                Debug.LogWarning("새로운 유저 정보를 서버에 저장하는데 실패했다.");
            }
        }
    }

    /// <summary>
    /// 캐릭터가 존재하는지 체크한다.
    /// </summary>
    /// <param name="name">캐릭터 이름</param>
    /// <param name="characterType">캐릭터 타입</param>
    /// <returns>응답 메세지</returns>
    public IEnumerator ICheckCharacterExist(string name, EPlayerCharacterType characterType)
    {
        Debug.Log("4.3 characterType : " + characterType);
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Character_Type", (int)characterType);

        UnityWebRequest www = UnityWebRequest.Post(CHECK_IF_USER_CHARACTER_EXIST_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log(" download handler : " + www.downloadHandler.text);

            JSONArray jsonArray = (JSONArray)JSON.Parse(www.downloadHandler.text);

            string message = jsonArray[0]["COUNT(User_Name)"];
            if (message == "1") // 1이면 존재한다
            {
                isExistCharacter[(int)characterType - 1] = true;
                Debug.Log("isExist 0 : " + isExistCharacter[0] + ", 1 : " + isExistCharacter[1]);
            }
        }
    }

    /// <summary>
    /// 유저의 새롭게 만든 캐릭터 데이터를 서버에 생성한다. (캐릭터를 새로 만들 때만 사용) 
    /// </summary>
    public void CreateNewUserCharacterDataToServer()
    {
        StartCoroutine(ICreateNewUserCharacterInformationToServer());
    }

    private IEnumerator ICreateNewUserCharacterInformationToServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("HP", PlayerInformation.userData.hp.ToString());
        form.AddField("ATK", PlayerInformation.userData.atk.ToString());
        form.AddField("DEF", PlayerInformation.userData.def.ToString());
        form.AddField("SPD_MOV", PlayerInformation.userData.moveSpeed.ToString());
        form.AddField("SPD_ATK", PlayerInformation.userData.attackSpeed.ToString());
        form.AddField("Gold", PlayerInformation.userData.gold);
        form.AddField("Crystal", PlayerInformation.userData.crystal);
        form.AddField("EXP", PlayerInformation.userData.exp.ToString());
        form.AddField("Level", PlayerInformation.userData.level);
        form.AddField("Stamina", PlayerInformation.userData.Stamina);

        UnityWebRequest www = UnityWebRequest.Post(CREATE_NEW_USER_CHARACTER_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            if (www.downloadHandler.text == RESPONSE_SUCCESS_MESSAGE)
            {
                Debug.Log("캐릭터의 기본 데이터 생성에 성공하였습니다." + www.downloadHandler.text);
            }
            else
            {
                Debug.LogWarning("캐릭터의 기본 데이터 생성에 실패하였습니다." + www.downloadHandler.text);
            }
        }
    }


    /// <summary>
    /// 유저의 장비 아이템 하나를 서버에 저장한다.
    /// 서버 저장 성공 시 아이템의 id 값을 가져와 item에 대입한다.
    /// </summary>
    /// <param name="item">장비 아이템</param>
    /// <returns>응답 메세지</returns>
    public IEnumerator ISaveEquipmentItemsToServer(EquipmentItem item)
    {
        Debug.Log("3:" + item.atk + " , " + item.name);
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Gold", item.gold);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("Item_Name", item.name);
        form.AddField("Tier", (int)item.tier);
        form.AddField("Equipment_Type", (int)item.type);
        form.AddField("ATK", item.atk.ToString());
        form.AddField("DEF", item.def.ToString());
        form.AddField("SPD_MOV", item.moveSpeed.ToString());
        form.AddField("SPD_ATK", item.attackSpeed.ToString());
        form.AddField("Tendency", (int)item.tendency);
        form.AddField("Reinforcement", item.reinforcementCount);
        if (PlayerInformation.userData.characterType == EPlayerCharacterType.archer)
        {
            form.AddField("Image_Number", item.imageNumber + 1000);
        }
        else
        {
            form.AddField("Image_Number", item.imageNumber);
        }

        UnityWebRequest www = UnityWebRequest.Post(SAVE_EQUIPMENTITEM_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("ddddddddd : " + www.downloadHandler.text);
            JSONObject jsonData = (JSONObject)JSON.Parse(www.downloadHandler.text);

            int message = jsonData["affectedRows"];

            if (message == 1) // 1이면 저장 성공
            {
                item.id = jsonData["insertId"];
                Debug.Log("서버에 아이템 저장 성공" + www.downloadHandler.text + " id: " + item.id);
            }
            else
            {
                Debug.LogWarning("서버에 아이템 저장 실패 : " + www.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// 유저의 모든 장비 아이템을 서버로부터 불러온다.
    /// </summary>
    public IEnumerator ILoadEquipmentItemsFromServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        UnityWebRequest www = UnityWebRequest.Post(LOAD_EQUIPMENTITEMS_URL, form);
        yield return www.SendWebRequest();

        //PlayerInformation.equipmentItemDataList
        if (www.isDone)
        {
            if (www.downloadHandler.text == "0") // 0이면 장비가 없다
            {
                Debug.Log("가지고 있는 장비가 없음");
            }
            else
            {
                JSONArray jsonArray = (JSONArray)JSON.Parse(www.downloadHandler.text);

                for (int i = 0; i < jsonArray.Count; ++i)
                {
                    EquipmentItem item = new EquipmentItem();
                    // skip "Name", "Character_Type"
                    item.id = jsonArray[i]["Item_Id"];
                    item.name = jsonArray[i]["Name"];
                    item.tier = (ETierType)(int)jsonArray[i]["Tier"];
                    item.type = (EEquipmentItemType)(int)jsonArray[i]["Equipment_Type"];
                    item.atk = jsonArray[i]["ATK"];
                    item.def = jsonArray[i]["DEF"];
                    item.moveSpeed = jsonArray[i]["SPD_MOV"];
                    item.attackSpeed = jsonArray[i]["SPD_ATK"];
                    item.gold = jsonArray[i]["Gold"];
                    item.tendency = (ETendency)(int)jsonArray[i]["Tendency"];
                    item.reinforcementCount = jsonArray[i]["Reinforcement"];
                    if (PlayerInformation.userData.characterType == EPlayerCharacterType.archer)
                    {
                        item.imageNumber = jsonArray[i]["Image_Number"] % 1000;
                    }
                    else
                    {
                        item.imageNumber = jsonArray[i]["Image_Number"];
                    }
                    PlayerInformation.inventory.equipmentItemDataList.Add(item);
                }
                Debug.Log("서버로부터 장비를 가져옴!");
            }

        }
    }

    /// <summary>
    /// 서버에 있는 장비 아이템을 삭제하게 한다.
    /// </summary>
    /// <param name="id">장비 아이템의 id</param>
    /// <returns>응답 메세지</returns>
    public IEnumerator IDeleteEquipmentItemToServer(int id)
    {
        Debug.Log("dddddddd");
        WWWForm form = new WWWForm();
        form.AddField("Item_Id", id);
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);

        UnityWebRequest www = UnityWebRequest.Post(DELETE_EQUIPMENTITEM_URL, form);

        yield return www.SendWebRequest();
        Debug.Log("eeeeeeeeee");
        if (www.isDone)
        {
            if (www.downloadHandler.text == RESPONSE_USER_NAME_EXISTS_MESSAGE)
            {
                Debug.Log("ffffffff");
                JSONObject doneData = (JSONObject)JSON.Parse(www.downloadHandler.text);
                int success = doneData["affectedRows"];
                if (success == 1) // 1이면 아이템이 정상적으로 삭제됨
                {
                    Debug.Log("장비 아이템 삭제 됨");
                }
                else
                {
                    Debug.Assert(success == 1, "삭제 할 장비 아이템 id를 찾지 못함");
                }
            }
        }
    }

    /// <summary>
    /// 유저의 캐릭터 데이터를 서버에서 불러와 플레이어 데이터에 저장한다.
    /// </summary>
    /// <param name="name">유저 이름</param>
    /// <param name="characterType">불러올 캐릭터 타입</param>
    /// <returns></returns>
    public IEnumerator ILoadCharacterDataFromServer(string name, EPlayerCharacterType characterType)
    {
        Debug.Log("코루틴 돌아감 ILoadUserDataFromServer");
        WWWForm form = new WWWForm();
        form.AddField("Name", name);
        form.AddField("Character_Type", (int)characterType);

        UnityWebRequest www = UnityWebRequest.Post(LOAD_CHARACTER_DATA, form);
        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("유저 데이터를 받아옴 : " + www.downloadHandler.text);

            // 서버에서 받아온 데이터를 저장.
            JSONArray json = (JSONArray)JSON.Parse(www.downloadHandler.text);

            PlayerInformation.userData.name = json[0]["User_Name"];
            //if (json[0]["Character_Type"] == EPlayerCharacterType.warrior.ToString())
            //{
            //    PlayerInformation.userData.characterType = EPlayerCharacterType.warrior;
            //}
            //else if (json[0]["Character_Type"] == EPlayerCharacterType.archer.ToString())
            //{
            //    PlayerInformation.userData.characterType = EPlayerCharacterType.archer;
            //}
            PlayerInformation.userData.hp = json[0]["HP"];
            PlayerInformation.userData.atk = json[0]["ATK"];
            PlayerInformation.userData.def = json[0]["DEF"];
            PlayerInformation.userData.moveSpeed = json[0]["SPD_MOV"];
            PlayerInformation.userData.attackSpeed = json[0]["SPD_ATK"];
            PlayerInformation.userData.gold = json[0]["Gold"];
            PlayerInformation.userData.crystal = json[0]["Crystal"];
            PlayerInformation.userData.exp = json[0]["EXP"];
            PlayerInformation.userData.level = json[0]["Level"];
            PlayerInformation.userData.Stamina = json[0]["Stamina"];
        }
    }

    /// <summary>
    /// 클라이언트에서 수정된 캐릭터의 데이터를 서버로 보내 저장하게 한다.
    /// (mysql에서 Insert가 아닌 Update쿼리를 실행시킨다)
    /// </summary>
    public IEnumerator ISaveCharacterDataToServer()
    {
        Debug.Log("Name : " + PlayerInformation.userData.name + " " + PlayerInformation.userData.name + " " + PlayerInformation.userData.characterType.ToString() + " " + PlayerInformation.userData.hp + " " + PlayerInformation.userData.exp.ToString());
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("HP", PlayerInformation.userData.hp.ToString());
        form.AddField("ATK", PlayerInformation.userData.atk.ToString());
        form.AddField("DEF", PlayerInformation.userData.def.ToString());
        form.AddField("SPD_MOV", PlayerInformation.userData.moveSpeed.ToString());
        form.AddField("SPD_ATK", PlayerInformation.userData.attackSpeed.ToString());
        form.AddField("Gold", PlayerInformation.userData.gold);
        form.AddField("Crystal", PlayerInformation.userData.crystal);
        form.AddField("EXP", PlayerInformation.userData.exp.ToString());
        form.AddField("Level", PlayerInformation.userData.level);
        form.AddField("Stamina", PlayerInformation.userData.Stamina);

        UnityWebRequest www = UnityWebRequest.Post(UPDATE_CHARACTER_DATA_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            if (www.downloadHandler.text == RESPONSE_SUCCESS_MESSAGE)
            {
                Debug.Log("character의 userData Update 완료");
            }
            else
            {
                Debug.Log("character의 userData Update 실패");
            }
        }
    }

    /// <summary>
    /// 소비 아이템 정보를 서버에 추가한다.
    /// 성공 할 경우 item의 id 값을 가져와 item에 대입한다.
    /// </summary>
    /// <returns>응답 메세지</returns>
    public IEnumerator IAddConsumableItemToServer(ConsumableItem item)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("Item_Name", item.name);
        form.AddField("Type", (int)item.type);
        form.AddField("Gold", item.gold);
        form.AddField("Image_Number", item.imageNumber);

        UnityWebRequest www = UnityWebRequest.Post(ADD_CONSUMABLE_ITEM_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            JSONObject jsonData = (JSONObject)JSON.Parse(www.downloadHandler.text);

            int message = jsonData["affectedRows"];

            if (message == 1)
            {
                item.id = jsonData["insertId"];
                Debug.Log("서버에 아이템 저장 성공" + www.downloadHandler.text);
            }
            else
            {
                Debug.LogWarning("서버에 아이템 저장 실패 : " + www.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// 소비 아이템들을 서버로부터 불러온다.
    /// </summary>
    /// <returns>응답 메세지</returns>
    public IEnumerator ILoadConsumableItemsFromServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);

        UnityWebRequest www = UnityWebRequest.Post(LOAD_CONSUMABLE_ITEMS_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            if (www.downloadHandler.text == "0") // 0 : 하나도 없을 경우
            {
                Debug.Log("서버에 소비 아이템이 하나도 없음. " + www.downloadHandler.text);
            }
            else
            {
                Debug.Log("서버에 소비 아이템들을 불러오는것 성공했다. " + www.downloadHandler.text);
                JSONArray jsonArray = (JSONArray)JSON.Parse(www.downloadHandler.text);
                // 인벤토리에 데이터를 넣어줌
                for (int i = 0; i < jsonArray.Count; ++i)
                {
                    ConsumableItem item = new ConsumableItem();

                    item.id = jsonArray[i]["Item_Id"];
                    item.name = jsonArray[i]["Name"];
                    item.type = (EConsumableItemType)(int)jsonArray[i]["Type"];
                    item.gold = jsonArray[i]["Gold"];
                    item.imageNumber = jsonArray[i]["Image_Number"];
                    PlayerInformation.inventory.consumableItemDataList.Add(item);
                }
            }
        }
    }

    /// <summary>
    /// 서버에 소비 아이템을 삭제한다.
    /// </summary>
    /// <param itemId="id">소비 아이템의 id</param>
    /// <returns></returns>
    public IEnumerator IDeleteConsumableItemFromServer(int itemId)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("Item_Id", itemId);

        UnityWebRequest www = UnityWebRequest.Post(DELETE_CONSUMABLE_ITEM_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("delete consumableItem:: ::: " + www.downloadHandler.text);
        }
    }
    /// <summary>
    /// 장비한 아이템들의 정보를 서버로부터 가져온다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ILoadEquippedItemsFromServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);

        UnityWebRequest www = UnityWebRequest.Post(LOAD_EQUIPPED_ITEMS_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            JSONArray jsonArray = (JSONArray)JSON.Parse(www.downloadHandler.text);

            for (int i = 0; i < jsonArray.Count; ++i)
            {
                EEquipmentItemType itemType = (EEquipmentItemType)(int)jsonArray[i]["Equipment_Type"];
                int itemId = jsonArray[i]["Equipped_Item_Id"];

                // id에 맞는 장비 아이템을 찾는다.
                int itemIndex = PlayerInformation.inventory.GetEquipmentItemId(itemId);
                if(itemIndex < 0)
                {
                    continue;
                }

                switch (itemType) // 타입별로 아이템 값 집어넣기
                {
                    case EEquipmentItemType.armor:
                        PlayerInformation.inventory.currentEquippedArmorOrNull.CopyData(PlayerInformation.inventory.equipmentItemDataList[itemIndex]);
                        break;
                    case EEquipmentItemType.helmet:
                        PlayerInformation.inventory.currentEquippedHelmetOrNull.CopyData(PlayerInformation.inventory.equipmentItemDataList[itemIndex]);
                        break;
                    case EEquipmentItemType.shoes:
                        PlayerInformation.inventory.currentEquippedShoesOrNull.CopyData(PlayerInformation.inventory.equipmentItemDataList[itemIndex]);
                        break;
                    case EEquipmentItemType.weapon:
                        PlayerInformation.inventory.currentEquippedWeaponOrNull.CopyData(PlayerInformation.inventory.equipmentItemDataList[itemIndex]);
                        break;
                    default:
                        Debug.LogWarning("타입 범위 초과!!!!!!!!!!!!!");
                        break;
                }
                Debug.Log("Network manager : 장착한 아이템들의 정보를 서버로부터 가져온다." + PlayerInformation.inventory.currentEquippedArmorOrNull.name + ", " + PlayerInformation.inventory.currentEquippedHelmetOrNull.name + ", " + PlayerInformation.inventory.currentEquippedShoesOrNull.name + ", " + PlayerInformation.inventory.currentEquippedWeaponOrNull.name);
            }
        }
    }

    /// <summary>
    /// 장비한 아이템의 정보를 서버로 저장한다.
    /// </summary>
    /// <param name="itemId">장비한 아이템의 id</param>
    /// <param name="type">장비한 아이템의 type</param>
    /// <returns></returns>
    public IEnumerator ISaveEquippedItemToServer(int itemId, EEquipmentItemType type)
    {
        yield return StartCoroutine(IDeleteEquippedItemToServer(type));

        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("Equipped_Item_Id", itemId);
        form.AddField("Equipment_Type", (int)type);

        UnityWebRequest www = UnityWebRequest.Post(SAVE_EQUIPPED_ITEM_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            if (www.downloadHandler.text == RESPONSE_SUCCESS_MESSAGE)
            {
                Debug.Log("현재 장비하고 있는 아이템을 서버로 저장했습니다." + www.downloadHandler.text);
            }
            else
            {
                Debug.LogWarning("저장하지 못했습니다!" + www.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// 장착 해제 한 아이템의 정보를 서버로 보내 서버에서 삭제하게 한다.
    /// </summary>
    /// <param name="itemId">삭제할 장비 아이템의 id</param>
    /// <param name="type">삭제할 장비 아이템의 type</param>
    /// <returns></returns>
    public IEnumerator IDeleteEquippedItemToServer(EEquipmentItemType type)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("Equipment_Type", (int)type);

        UnityWebRequest www = UnityWebRequest.Post(DELETE_EQUIPPED_ITEM_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("delete equipmentItem ::: " +www.downloadHandler.text);
        }
    }

    /// <summary>
    /// 스테이지의 정보를 설정한다.
    /// </summary>
    /// <param name="stageNumber">설정 할 스테이지의 번호</param>
    /// <param name="starCount">설정 할 별의 개수</param>
    /// <returns></returns>
    public IEnumerator ISetStageInformationToServer(int stageNumber, int starCount)
    {
        stageNumber = Mathf.Clamp(stageNumber, 0, PlayerInformation.stageData.maxStageCount);
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);
        form.AddField("Stage_Number", stageNumber);
        form.AddField("Star_Count", starCount);

        UnityWebRequest www = UnityWebRequest.Post(SET_STAGE_NUMBER_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("Success" + www.downloadHandler.text);
        }
    }

    /// <summary>
    /// 서버에서 저장되어있는 캐릭터의 스테이지의 정보를 모두 가져온다.
    /// </summary>
    /// <returns></returns>
    public IEnumerator IGetStagesInformation()
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", PlayerInformation.userData.name);
        form.AddField("Character_Type", (int)PlayerInformation.userData.characterType);

        UnityWebRequest www = UnityWebRequest.Post(GET_STAGES_INFORMATION_URL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("가져온 스테이지의 정보 : " + www.downloadHandler.text);
        }
    }
}
