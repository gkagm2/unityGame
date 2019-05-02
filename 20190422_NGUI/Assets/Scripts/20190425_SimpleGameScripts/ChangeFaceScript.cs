using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFaceScript : MonoBehaviour {
    public UISprite face;
    public short maxImageCount = 7;

    public float coolTime =0f ;
    public float maxTime = 5.0f;

    int i;


	// Use this for initialization
	void Start () {
        i = 0;
	}
	
	// Update is called once per frame
	void Update () {
        

        coolTime += Time.deltaTime;

        if (coolTime > maxTime)
        {
            coolTime = 0;
            ++i;
            face.spriteName = "d" + i;
            if (i > maxImageCount + 1)
                i = 0;
            maxTime = Random.Range(2f, 4f);
        }

    }
}
