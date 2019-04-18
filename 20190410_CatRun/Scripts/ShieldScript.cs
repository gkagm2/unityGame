using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0.0f, 0.0f, 30.0f);
	}
    //방패를 위치에 뿌려줌
    public void PickShield(Transform playerPosition)
    {
        transform.position = new Vector3(playerPosition.position.x + 1.0f, playerPosition.position.y, playerPosition.position.z);
    }
}
