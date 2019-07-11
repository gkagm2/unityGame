using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ObjectController {


	// Update is called once per frame
	void Update () {
        if (BallGameManager.instance.isPlayerFail)
            return;
        
        DestoryWhenArriveDestination();
        Move();
    }

    public override void SetMoveMent(LevelState currentLevel)
    {
        base.SetMoveMent(currentLevel);

        switch (currentLevel)
        {
            case LevelState.level1:
                rotationSpeedZ = 0;
                break;
            case LevelState.level2:
                rotationSpeedZ = 50f * Random.Range(-1, 2) * Time.deltaTime; // 1, 0, -1값으로 회전, 회전 방향 주기
                break;
            case LevelState.level3:
                rotationSpeedZ = 60f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelState.level4:
                rotationSpeedZ = 70f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelState.level5:
                rotationSpeedZ = 80f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelState.level6:
                rotationSpeedZ = 90f * Random.Range(-1, 2) * Time.deltaTime;
                break;
            case LevelState.level7:
                rotationSpeedZ = 100f * Random.Range(-1, 2) * Time.deltaTime;
                break;
        }
    }

}
