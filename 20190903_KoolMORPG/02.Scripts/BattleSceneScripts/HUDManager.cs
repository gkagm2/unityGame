using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    public Transform camTr;
    public Transform tr;
    public PlayerMovement pm;
    public GameObject pausePopup;
    public Image skill1;
    public Image skill2;

    public Image playerHpBar;
    public Image playerFace;

    public float playerMaxHp;

    public Text playerHpPotionCountText; // 포션 개수 Text

    void Start()
    {
        camTr = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        tr = GetComponent<Transform>();
        UiImageInitialize();
    }

    public void HUDInitialize()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }
    public void GetPlayerMaxHp()
    {
        playerMaxHp = pm.hp;
    }

    public void PlayerHpUpdate()
    {
        playerHpBar.fillAmount = pm.hp/playerMaxHp;
        playerHpPotionCountText.text = PlayerInformation.inventory.GetHpPotionCount().ToString();
    }

    public void UseHpPotion()
    {
        if (playerHpBar.fillAmount == 1 || PlayerInformation.inventory.GetHpPotionCount() <= 0)
        {
            return;
        }
        StartCoroutine(IUseHpPotion());
    }
    private IEnumerator IUseHpPotion()
    {
        int hpPotionItemId = PlayerInformation.inventory.UseHpPotionItem(); // 아이템 사용 후 아이템의 아이디를 받아옴
        Debug.Log("hpPotionItemId : " + hpPotionItemId);
        if (NetworkManager.instance != null)
        {
            yield return NetworkManager.instance.IDeleteConsumableItemFromServer(hpPotionItemId); // 받아온 아이템의 아이디로 서버에 삭제하게 함.
        }
        pm.UseHpPotionParticle();
        pm.hp = playerMaxHp;
        if (pm.hp >= playerMaxHp)
        {
            pm.hp = playerMaxHp;
        }
        PlayerHpUpdate();
    }

    public void PausePopUp()
    {
        if(pausePopup.activeSelf == false)
        {
            pausePopup.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePopup.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void MainSceneLoadBtn()
    {
        PausePopUp();
        SceneMoveManager.LoadScene("MainScene", "Map_MainScene");
    }

    //public void SettingPopUp()
    //{
    //    Debug.Log("세팅 팝업");
    //}

    public void UiImageInitialize()
    {
        switch (PlayerInformation.userData.characterType)
        {
            case EPlayerCharacterType.none:
                break;
            case EPlayerCharacterType.warrior:
                skill1.sprite = Resources.Load<Sprite>("Images/SkillImage/WarriorSkill01");
                skill2.sprite = Resources.Load<Sprite>("Images/SkillImage/WarriorSkill02");
                PlayerInformation.userData.ChangeFaceImage(EPlayerCharacterType.warrior);
                playerFace.sprite = PlayerInformation.userData.characterFaceIcon;

                break;
            case EPlayerCharacterType.archer:
                skill1.sprite = Resources.Load<Sprite>("Images/SkillImage/ArcherSkill01");
                skill2.sprite = Resources.Load<Sprite>("Images/SkillImage/ArcherSkill02");
                PlayerInformation.userData.ChangeFaceImage(EPlayerCharacterType.archer);
                playerFace.sprite = PlayerInformation.userData.characterFaceIcon;
                break;
            default:
                break;
        }
        playerHpPotionCountText.text = PlayerInformation.inventory.GetHpPotionCount().ToString();
    }
}
