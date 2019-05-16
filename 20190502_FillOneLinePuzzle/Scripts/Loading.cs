using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {
    public GameObject loadingScreen;
    public GameObject stageScreen;
    public UILabel loadingText;
	// Use this for initialization
	void Awake () {
        StartCoroutine("InitSetting");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator InitSetting()
    {
        loadingText.text = "Loading";
        yield return new WaitForSecondsRealtime(0.5f);
        loadingText.text = "Loading . ";
        yield return new WaitForSecondsRealtime(0.5f);
        GameObject[] maps = GameObject.FindGameObjectsWithTag("Map");
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(false);
        }
        loadingText.text = "Loading . .";
        yield return new WaitForSecondsRealtime(0.5f);

        stageScreen.SetActive(false);
        loadingScreen.SetActive(false);
    }
}
