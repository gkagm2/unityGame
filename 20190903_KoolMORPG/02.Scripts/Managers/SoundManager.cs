using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{      
    public static SoundManager Instance = null;
    public AudioSource playingBackGroundMusic;
    public AudioSource mainSceneBackGroundMusic;
    public AudioSource battleSceneBackGroundMusic;

    public GameObject mainSceneBackGroundMusicObject;
    public GameObject battleSceneBackGroundMusicObject;

    /// <summary>
    /// 싱글톤
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);        
    }

    /// <summary>
    /// 시작시 사운드의 on/off 체크
    /// </summary>
    void Start()
    {
        mainSceneBackGroundMusic = mainSceneBackGroundMusicObject.GetComponent<AudioSource>();
        battleSceneBackGroundMusic = battleSceneBackGroundMusicObject.GetComponent<AudioSource>();

        //배경음 on/off 체크
        if (PlayerPrefs.GetInt("BackGroundMusicSoundCheck") == 0)
        {
            BackGroundMusicSoundOff();            
        }
        else
        {
            BackGroundMusicSoundOn();           
        }
        //효과음 on/off 체크
        if (PlayerPrefs.GetInt("SoundEffectCheck") == 0)
        {
            SoundEffectOff();
        }
        else
        {
            SoundEffectOn();
        }    
    }

    // Update is called once per frame    
    void Update()
    {
        /// <summary>
        /// 씬 유형에 따라 체크
        /// </summary>        
        // 메인 씬 인 경우
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            playingBackGroundMusic = mainSceneBackGroundMusic;
            battleSceneBackGroundMusic.mute = true;
            battleSceneBackGroundMusicObject.SetActive(false);
            mainSceneBackGroundMusic.mute = false;
            mainSceneBackGroundMusicObject.SetActive(true);
            if (PlayerPrefs.GetInt("BackGroundMusicSoundCheck") == 0)
            {
                playingBackGroundMusic.mute = true;
            }
            else
            {
                playingBackGroundMusic.mute = false;
            }
        }
        //배틀 씬 인 경우
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            playingBackGroundMusic = battleSceneBackGroundMusic;
            battleSceneBackGroundMusic.mute = false;
            battleSceneBackGroundMusicObject.SetActive(true);
            mainSceneBackGroundMusic.mute = true;
            mainSceneBackGroundMusicObject.SetActive(false);
            if (PlayerPrefs.GetInt("BackGroundMusicSoundCheck") == 0)
            {
                playingBackGroundMusic.mute = true;
            }
            else
            {
                playingBackGroundMusic.mute = false;
            }
        }
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
