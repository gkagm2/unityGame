using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopupPanelDisplay : MonoBehaviour
{
    public GameObject optionBackGroundMusicButtonOn;
    public GameObject optionBackGroundMusicButtonOff;
    public GameObject optionSoundEffectButtonButtonOn;
    public GameObject optionSoundEffectButtonButtonOff;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("BackGroundMusicSoundCheck") == 0)
        {
            optionBackGroundMusicButtonOff.SetActive(true);
            optionBackGroundMusicButtonOn.SetActive(false);
        }
        else
        {
            optionBackGroundMusicButtonOff.SetActive(false);
            optionBackGroundMusicButtonOn.SetActive(true);
        }
        //효과음 on/off 체크
        if (PlayerPrefs.GetInt("SoundEffectCheck") == 0)
        {
            optionSoundEffectButtonButtonOff.SetActive(true);
            optionSoundEffectButtonButtonOn.SetActive(false);
        }
        else
        {
            optionSoundEffectButtonButtonOff.SetActive(false);
            optionSoundEffectButtonButtonOn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 배경음 Mute On/Off
    /// </summary>
    public void BackGroundMusicSoundOn()
    {
        PlayerPrefs.SetInt("BackGroundMusicSoundCheck", 1);
    }
    public void BackGroundMusicSoundOff()
    {
        PlayerPrefs.SetInt("BackGroundMusicSoundCheck", 0);
    }
    /// <summary>
    /// 효과음 Mute On/off
    /// </summary>
    public void SoundEffectOn()
    {
        PlayerPrefs.SetInt("SoundEffectCheck", 1);
    }
    public void SoundEffectOff()
    {
        PlayerPrefs.SetInt("SoundEffectCheck", 0);
    }
}
