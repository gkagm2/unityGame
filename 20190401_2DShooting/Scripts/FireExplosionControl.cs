using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosionControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyThis());
    }

    // Update is called once per frame
    void Update () {
        
	}
    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}
