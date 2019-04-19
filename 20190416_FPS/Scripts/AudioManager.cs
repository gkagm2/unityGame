using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    static AudioManager _instance = null;
    public static AudioManager Instance()
    {
        return _instance;
    }
    public AudioClip music = null;
	// Use this for initialization
	void Start () {
		if(_instance == null)
        {
            _instance = this;
        }
        if (music != null) //Background Music BGM
        {
            //Debug.Log("aaa");
            GetComponent<AudioSource>().clip = music;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }
	}
	
    public void PlaySfx(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
