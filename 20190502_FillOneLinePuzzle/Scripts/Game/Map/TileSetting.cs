using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetting : MonoBehaviour {
    UISprite uiSprite;

	// Use this for initialization
	void Start () {
        uiSprite = GetComponent<UISprite>();
        uiSprite.depth = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
