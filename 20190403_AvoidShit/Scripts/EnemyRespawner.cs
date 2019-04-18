using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRespawner : MonoBehaviour {
    float CoolTime = 1.0f;
    float respawnCoolTime = 1.0f;

    [Header("Enemy Object")]
    public GameObject enemyObj;

    [Header("Enemy respawn setting")]
    public float movementDistance = 4.0f; //리스폰 되는 좌 우 거리 최대치 설정
    public float respawnHeight = 12.0f; //리스폰 높이 거리 
    public float maxRespawnCoolTime = 1; //마지막 Level에 Enemy가 리스폰 될 최대 주기(cool time)


	void Update () {
        CoolTime += Time.deltaTime;

        //플레이어가 살아있으면
        if (PlayerControl.alive)
        {
            if (CoolTime > maxRespawnCoolTime)
            {
                //x값을 랜덤으로 받아오기 
                transform.position = new Vector3(Random.Range(-movementDistance, movementDistance), 16, 0);
                if (maxRespawnCoolTime > 0.05)
                {
                    maxRespawnCoolTime -= 0.05f;
                }
                CoolTime = 0;
            }

            respawnCoolTime += Time.deltaTime;
            if (respawnCoolTime > maxRespawnCoolTime)
            {

                Instantiate(enemyObj, transform.position, transform.rotation);
                if (maxRespawnCoolTime > 0.05)
                {
                    maxRespawnCoolTime -= 0.05f;
                }
                respawnCoolTime = 0;
            }
        }/// TODO : Player가 죽으면 
        else { }
    }
}
