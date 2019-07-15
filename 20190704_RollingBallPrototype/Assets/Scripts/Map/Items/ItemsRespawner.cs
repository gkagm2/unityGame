using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsRespawner : RespawnController {

    public Transform respawnPosition; // 리스폰 할 위치

    public GameObject[] itemObj;

	// Update is called once per frame
	void Update () {
        if (BallGameManager.instance.isPlayerFail)
            return;

        RespawnObjectAtRegularCycle(respawnMaxTime);
    }

    // 일정한 주기마다 오브젝트 생성
    public override void RespawnObjectAtRegularCycle(float coolTime)
    {
        respawnTimer += Time.deltaTime;

        if(respawnTimer >= coolTime)
        {
            CreateObject();
            respawnTimer = 0;
        }
    }
    


    // 아이템 생성

    public override void CreateObject()
    {
        base.CreateObject();

        // TODO : 레벨 별로 아이템 움직임 제어.
        
        GameObject respawnedItem = Instantiate(itemObj[Random.Range(0, itemObj.Length)], respawnPosition.position, respawnPosition.rotation);
        if (respawnedItem) // 생성 될 시
        {
            ChangeLevel(Level.currentLevel);
            respawnedItem.GetComponent<Item>().SetMoveMent(Level.currentLevel); // 현재 레벨 세팅해준다.
            respawnedItem.transform.Rotate(0, 0, Random.Range(0, 360), Space.World); // 생성 할 때의 각도 설정
        }
    }
}
