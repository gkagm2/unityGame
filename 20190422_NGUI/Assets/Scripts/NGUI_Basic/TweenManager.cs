using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenManager : MonoBehaviour {
    public GameObject tweenTarget;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tweenTarget.GetComponent<TweenAlpha>().PlayForward();
            tweenTarget.GetComponent<TweenScale>().PlayForward();
            tweenTarget.GetComponent<TweenScale>().style = UITweener.Style.Loop;
            tweenTarget.GetComponent<TweenScale>().to = new Vector3(2f,2f,2f);
        }
	}
}
