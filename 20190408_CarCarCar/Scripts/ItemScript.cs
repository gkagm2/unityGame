using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {
    
    GameManager gameManager;
    public float itemSpeed =10.0f;
    public int itemType;

    public GameObject []item;
    
	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //모든 아이템 감추기
        foreach(GameObject itm in item) 
        {
            itm.SetActive(false);
        }
        itemType = Random.Range(0, 3); //아이템 타입을 랜덤으로 받음.
        item[itemType].SetActive(true); //랜덤으로 선택 된 아이템 나타나기
    }
	
	// Update is called once per frame
	void Update () {
        float moveAmt = gameManager.scrollSpeed * itemSpeed; // 아이템 속도 계산
        
        transform.Translate(0, 0, -moveAmt * Time.deltaTime); //아이템 이동

        if(transform.position.z < -6.0f)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") //플레이어와 아이템이 충돌 시 
        {
            //플레이어에게 아이템 적용하기
            ApplyItemToPlayer();

            //아이템 파괴
            Destroy(gameObject);
        }
    }
    
    //플레이어에게 아이템 적용
    void ApplyItemToPlayer()
    {
        //현재 플레이어의 PlayerControl 컴포넌트를 받아온다.
        PlayerControl player = GameObject.Find("Player").GetComponent<PlayerControl>();


        /// TODO :
        /// ItemList 클래스랑 이 스크립트에 있는 Item 배열을 리펙토링 해야 될 듯.
        ///

        Debug.Log("Get Item Type : " + itemType);
        //아이템 선택 시 
        switch (itemType)
        {
            case 0: //Jerry 
                Debug.Log("Jerry item획득");
                break;
            case 1: //FirstAid
                Debug.Log("booster have one");
                player.haveBooster = true; 
                break;
            case 2: //Bottle(booster)
                player.RepareCar();
                break;

        }
    }
}
