using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRespawner : RespawnController {

    public Transform respawnPosition;
    public GameObject coinObj;
	
	// Update is called once per frame
	void Update () {
        if (BallGameManager.instance.isPlayerFail)
            return;

        RespawnObjectAtRegularCycle(respawnMaxTime); // 일정 주기마다 벽 생성
	}
    public override void RespawnObjectAtRegularCycle(float coolTime)
    {
        base.RespawnObjectAtRegularCycle(coolTime);
        respawnTimer += Time.deltaTime;
        if (respawnTimer >= coolTime) {
            CreateObject();
            respawnTimer = 0;
        }
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
