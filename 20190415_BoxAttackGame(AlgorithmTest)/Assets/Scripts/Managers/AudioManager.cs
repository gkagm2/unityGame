using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    static AudioManager _instance = null;


    public static AudioManager Instance()
    {
        return _instance;
    }

	// Use this for initialization
	void Start () {
        if (_instance == null)
        {
            _instance = this;
        }
	}
	public void PlaySfx(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
