using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ObjectController {

	
	// Update is called once per frame
	void Update () {
        if (BallGameManager.instance.isPlayerFail)
            return;

        DestroyWhenArriveDestination();
        Move();
	}

}
