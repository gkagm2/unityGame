using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotate : MonoBehaviour {

    public float RotateSpeed = 1.2f;


	// Update is called once per frame
	void Update () {
        if(BallGameManager.instance.isPlayerFail)
            return;

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
	}
}
