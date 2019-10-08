using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main 화면에 3D 캐릭터를 보여주는 클래스
/// </summary>
public class CharacterView : MonoBehaviour
{
    [Header("전사 캐릭터")]
    public GameObject[] warriorCharacters;
    public int warriorSkinNumber = 0;
    
    [Header("궁수 캐릭터")]
    public GameObject[] archarCharacters;
    public int archarSkinNumber = 0;


    void Start()
    {
        InitCharacter(PlayerInformation.userData.characterType);
    }

    /// <summary>
    /// 캐릭터 스킨을 설정된 캐릭터 타입에 따라 초기화한다.
    /// </summary>
    /// <param name="characterType">캐릭터 타입</param>
    public void InitCharacter(EPlayerCharacterType characterType)
    {
        switch (characterType)
        {
            case EPlayerCharacterType.archer:
                for(int i=0;i < warriorCharacters.Length; i++)
                {
                    SetArcharSkin(archarSkinNumber);
                }
                break;
            case EPlayerCharacterType.warrior:
                for(int i=0;i < archarCharacters.Length; i++)
                {
                    SetWarriorSkin(warriorSkinNumber);
                }
                break;
            case EPlayerCharacterType.none:
                for (int i = 0; i < warriorCharacters.Length; i++)
                {
                    SetArcharSkin(archarSkinNumber);
                }
                break;
            default:
                Debug.Assert(false, "캐릭터 선택이 안됨");
                break;
        }
    }

    /// <summary>
    /// 전사의 스킨을 설정한다.
    /// </summary>
    /// <param name="skinNumber">스킨 번호(default = 0)</param>
    private void SetWarriorSkin(int skinNumber = 0)
    {
        // 배열 index 범위를 벗어나면 0(default skin)으로 초기화
        if(skinNumber < 0 || skinNumber >= warriorCharacters.Length)
        {
            skinNumber = 0;
            Debug.LogWarning("전사 스킨 설정할 때 배열의 index 범위를 벗어남.");
        }
        for(int i=0; i < warriorCharacters.Length; ++i)
        {
            if(skinNumber == i)
            {
                warriorCharacters[i].SetActive(true);
            }
            else
            {
                warriorCharacters[i].SetActive(false);
            }
        }
        for (int i = 0; i < archarCharacters.Length; ++i)
        {
            archarCharacters[i].SetActive(false);
        }
    }

    /// <summary>
    /// 궁수의 스킨을 설정한다.
    /// </summary>
    /// <param name="skinNumber">스킨 번호(default = 0)</param>
    private void SetArcharSkin(int skinNumber= 0)
    {
        // 배열 index 범위를 벗어나면 0(default skin)으로 초기화
        if (skinNumber < 0 || skinNumber >= warriorCharacters.Length)
        {
            skinNumber = 0;
            Debug.LogWarning("궁수 스킨 설정할 때 배열의 index 범위를 벗어남.");
        }
        for (int i=0; i < archarCharacters.Length; ++i)
        {
            if(skinNumber == i)
            {
                archarCharacters[i].SetActive(true);
            }
            else
            {
                archarCharacters[i].SetActive(false);
            }
        }
        for (int i = 0; i < warriorCharacters.Length; ++i)
        {
            warriorCharacters[i].SetActive(false);
        }
    }
}
