using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRespawner : RespawnController {

    public Transform respawnPosition;

    public GameObject coinObj;
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void CreateObject()
    {
        base.CreateObject();

        GameObject respawnedCoin = Instantiate(coinObj, respawnPosition.position, respawnPosition.rotation);
        if (respawnedCoin) // 생성 될 시
        {
            ChangeLevel(Level.currentLevel);
            //respawnedCoin.GetComponent<>
        }

    }
}
