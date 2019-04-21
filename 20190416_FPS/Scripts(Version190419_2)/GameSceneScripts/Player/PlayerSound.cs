using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    // Sound
    public AudioClip[] hitSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HitSoundByEnemy()
    {
        GetComponent<AudioSource>().clip = hitSound[Random.Range(0,3)];
        GetComponent<AudioSource>().Play();
    }
    
}
