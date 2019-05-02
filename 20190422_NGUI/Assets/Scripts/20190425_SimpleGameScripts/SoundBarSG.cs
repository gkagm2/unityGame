using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBarSG : MonoBehaviour {
    public GameObject backgroundMusicObj;
    AudioSource audioSource;

    public UISlider musicVolumeSlider;
	// Use this for initialization
	void Start () {
        backgroundMusicObj = GameObject.Find("BackgroundMusic");
        audioSource = backgroundMusicObj.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        audioSource.volume = musicVolumeSlider.value;

    }
}
