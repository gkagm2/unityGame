using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMrgSG : MonoBehaviour {

    // Objects
    public GameObject []toggle;
    GameObject backgroundMusicSource;

    // paramerters
    public bool backgroundmusicflag;
    public bool lightflag;

    // Materials
    public Material material;
    public Light[] bimLight;


    // Use this for initialization
    void Start() {
        backgroundMusicSource = GameObject.Find("BackgroundMusic");
    }

    // Update is called once per frame
    void Update() {

    }
    
    public void MusicMuteToggle()
    {
        // backgound music mute 
        if(toggle[0].GetComponent<UIToggle>().name == "CheckBoxMute")
        {
            Debug.Log("group 1 toggle start");
            Debug.Log("value : " + toggle[0].GetComponent<UIToggle>().value);
            if (toggle[0].GetComponent<UIToggle>().value) //체크가 되어있으면
            {

                backgroundMusicSource.GetComponent<AudioSource>().mute = true; //뮤트 시키기
            }
            else //체크가 안되어 있으면
            {
                backgroundMusicSource.GetComponent<AudioSource>().mute = false; //뮤트 해제
            }
        }

    }
    public void ChangeColorToggle()
    {

        if(toggle[1].GetComponent<UIToggle>().name == "CheckBoxChangeLight") 
        {

            Debug.Log("group2 toggle start");
            Debug.Log("value : " + toggle[1].GetComponent<UIToggle>().value);
            if (toggle[1].GetComponent<UIToggle>().value)
            {
                material.color = Color.green;
                bimLight[1].color = Color.green;
                bimLight[0].color = Color.green;
            }
            else
            {
                material.color = Color.blue;
                bimLight[1].color = Color.blue;
                bimLight[0].color = Color.blue;
            }
        }
    }
}
