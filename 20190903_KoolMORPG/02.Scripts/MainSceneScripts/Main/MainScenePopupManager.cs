using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScenePopupManager : MonoBehaviour
{
    [Header("Inventory Panel")]
    public GameObject inventoryPanel;

    [Header("EquipmentMenuPopup")]
    private GameObject equipmentMenuPopup;
    private bool equipmentButtonClick; 
    
    [Header("SwitchMenuPopup")]
    private GameObject switchMenuPopup;
    private bool switchButtonClick;

    [Header("ShopPopup")]
    public GameObject shopMenuPopup;
    private bool shopButtonClick;

    [Header("AdventurePopup")]
    public GameObject adventurePopup;

    [Header("Camera Setting")]
    public Camera cameraMain;
    public Camera cameraUI;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        equipmentMenuPopup = GameObject.Find("EquipmentMenuPopup");
        switchMenuPopup = GameObject.Find("SwitchMenuPopup");
        InitMainUI();
    }

    // Main UI를 초기화해준다.
    public void InitMainUI()
    {
        equipmentMenuPopup.SetActive(false);
        switchMenuPopup.SetActive(false);
    }

    ///////////////// Panel ///////////////
    
    /// <summary>
    /// 인벤토리 창을 On/Off 하는 이벤트 함수
    /// </summary>
    /// <param name="">값의 여부에 따라 열고 닫힘</param>
    public void OnClick_InventoryPanelBtn(bool isOpen)
    {
        if (isOpen)
        {
            canvas.worldCamera = cameraUI.GetComponent<Camera>();
            //cameraUI.transform.position = new Vector3(-1.135f, 0.849f, 8f);

            cameraUI.depth = 1.0f;
            cameraMain.depth = 0.0f;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            inventoryPanel.SetActive(true);
        }
        else // 평소에는 Main카메라로 해야 함. 
        {
            canvas.worldCamera = cameraMain.GetComponent<Camera>();
            //cameraUI.transform.position = new Vector3(-1.135f, 0.849f, 5.0f);
            cameraUI.depth = 0.0f;
            cameraMain.depth = 1.0f;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            inventoryPanel.SetActive(false);
        }
    }

    /////////////// EquipmentMenuPopup ////////////////

    // 장비 팝업창 on/off

    public void ClickEquipmentButton()
    {
        equipmentButtonClick = !equipmentButtonClick;
        if (equipmentButtonClick)
        {
            equipmentMenuPopup.SetActive(true);
            switchButtonClick = false;
            switchMenuPopup.SetActive(false);
        }
        else
        {
            equipmentMenuPopup.SetActive(false);
        }
    }

    /////////////// SwitchMenuPopup ////////////////
    
    // 전환 팝업창 on/off

    public void ClickSwitchButton()
    {
        switchButtonClick = !switchButtonClick;
        if (switchButtonClick)
        {
            switchMenuPopup.SetActive(true);
            equipmentButtonClick = false;
            equipmentMenuPopup.SetActive(false);
        }
        else
        {
            switchMenuPopup.SetActive(false);
        }
    }

    /// <summary>
    /// 캐릭터 선택 버튼 클릭 시 호출
    /// </summary>
    public void OnClick_CharacterChoiceBtn()
    {
        SceneMoveManager.LoadScene("CharacterChoiceScene");
    }

    /////////////// ShopMenuPanelPopup ////////////////

    // 상점 패널창 on/off

    public void ClickShopButton()
    {
       shopButtonClick = !shopButtonClick;
        if (shopButtonClick)
        {
            shopMenuPopup.SetActive(true);

            equipmentButtonClick = false;
            equipmentMenuPopup.SetActive(false);
            switchButtonClick = false;
            switchMenuPopup.SetActive(false);
        }
        else
        {
            shopMenuPopup.SetActive(false);
        }
    }

    /////////////// AdventurePanelPopup ////////////////
    
    // 모험 팝업창 on/off
    public void OnClick_AdventureBtn(bool isOpen)
    {
        if (isOpen) 
        {
            adventurePopup.SetActive(true);
            adventurePopup.GetComponent<AdventurePopupManager>().UpdateUI();
        }
        else
        {
            adventurePopup.SetActive(false);
        }
    }
}
