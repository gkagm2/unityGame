using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ObjectController {



	// Update is called once per frame
	void Update () {
        if (BallGameManager.instance.isPlayerFail)
            return;
        
        DestroyWhenArriveDestination();
        Move();
    }

    public override void SetMoveMent(LevelStatus currentLevel)
    {
        base.SetMoveMent(currentLevel);

        rotationSpeedZ = 10f * Time.deltaTime;
    }

}
