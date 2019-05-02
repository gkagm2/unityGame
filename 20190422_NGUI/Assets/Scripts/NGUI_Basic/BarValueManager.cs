using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarValueManager : MonoBehaviour {
    public UIProgressBar bar;
    public float loadingTime = 0; // start 0]

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        loadingTime += Time.deltaTime;
        bar.value = loadingTime;
	}
}
