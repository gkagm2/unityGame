using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MORPG_CharacterInfoUI : MonoBehaviour
{
    public Text levelText;
    public RectTransform hpBarImage;
    public RectTransform mpBarImage;
    public Image hungerImage;

    public Button characterStateButton;

    [Range(0f, 1f)]
    public float hpBarState = 1f;

    [Range(0f, 1f)]
    public float mpBarState =1f;



    private void Update()
    {
        hpBarImage.localScale =  new Vector3(hpBarState, 1f ,1f);
        mpBarImage.localScale = new Vector3(mpBarState, 1f, 1f);
    }

    /// <summary>
    /// 캐릭터 정보 버튼을 클릭 시 호출
    /// </summary>
    public void OnClick_CharacterStatBtn(bool bIsOpen)
    {
        if (bIsOpen)
        {

        }
        else
        {
            
        }
    }
}
