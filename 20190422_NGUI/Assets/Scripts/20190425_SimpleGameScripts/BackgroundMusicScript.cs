using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour {
    public static BackgroundMusicScript _instance = null;
	// Use this for initialization
	void Start () {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
