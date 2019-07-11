using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 벽에 부딪히면
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            BallGameManager.instance.FailGame(); // 게임 실패
        }
        else if (other.tag == "BoostItem")
        {

        }
        else if(other.tag == "Coin")
        {

        }
    }
}
