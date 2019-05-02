using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMrgSG : MonoBehaviour {


    // InputWord
    public GameObject textBalloon;
    public UILabel balloonText;
    public UILabel inputText;


    // Use this for initialization
    void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	}
    public void OnClickShootingBtn()
    {
        Debug.Log("Shooting clicked!");
        FireGunScript gun = GameObject.Find("Player").transform.Find("FireGun").GetComponent<FireGunScript>();
        
        if (gun)
        {
            Debug.Log("success");
            gun.Fire();
        }
        
    }

    public void OnClickGotoSecondSceneBtn()
    {
        GameObject backgroundMusic = GameObject.Find("BackgroundMusic");
        DontDestroyOnLoad(backgroundMusic);
        SceneManager.LoadScene(1);
    }

    public void OnClickTextEnterBtn()
    {
        textBalloon.SetActive(true);
        balloonText.text = inputText.text;
        inputText.text = "";
        StartCoroutine(ClickTextEnterBtn());
    }
    IEnumerator ClickTextEnterBtn()
    {
            yield return new WaitForSeconds(3.0f);
            textBalloon.SetActive(false);
    }
    
    public void OnClickDrumStartBtn()
    {
        GameObject backgroundMusic = GameObject.Find("BackgroundMusic");
        DontDestroyOnLoad(backgroundMusic);
        SceneManager.LoadScene(2);
    }
    
}
