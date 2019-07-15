using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRespawner : RespawnController {


    public GameObject wallRespawnEffect; // 벽이 생성될 때 나오는 effect
    public GameObject[] wallObj;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (BallGameManager.instance.isPlayerFail)
            return;


        RespawnObjectAtRegularCycle(respawnMaxTime); // 일정 주기마다 벽 생성

    }

    // 일정한 주기마다 오브젝트 생성
    public override void RespawnObjectAtRegularCycle(float coolTime)
    {
        respawnTimer += Time.deltaTime;

        if(respawnTimer >= coolTime){
            CreateObject();
            respawnTimer = 0;
        }
    }



    public override void CreateObject()
    {
        base.CreateObject();

        // TODO : 레벨 별로 벽 움직임 제어.
        Quaternion qRotation = Quaternion.Euler(90f, 0f, 0f);

        GameObject respawnedWall = Instantiate(wallObj[Random.Range(0, wallObj.Length)], transform.position, qRotation);
        if (respawnedWall) // 생성 될 시
        {
            ChangeLevel(Level.currentLevel);
            respawnedWall.GetComponent<Wall>().SetMoveMent(Level.currentLevel); // 현재 레벨 세팅해준다.
            respawnedWall.transform.Rotate(0, 0, Random.Range(0, 360), Space.World); // 생성 할 때의 각도 설정
        }
    }
}
