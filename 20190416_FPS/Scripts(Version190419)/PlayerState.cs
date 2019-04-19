using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public int hp = 5;
    public int maxhp = 5;
    public bool isDead = false;

    //camera shake
    CameraShake cameraShake = null;

	// Use this for initialization
	void Start () {
        cameraShake = GetComponentInChildren<CameraShake>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageByEnemy()
    {
        if (isDead) //죽으면 
            return;//끝

        --hp;

        cameraShake.PlayCameraShake();


        if (hp <= 0)
        {
            isDead = true;
            
            //결과 Scene으로 이동
            SceneMrg sceneManager = GameObject.Find("GameManager").GetComponent<SceneMrg>();
            sceneManager.GotoResultScene();

        }
    }
}
