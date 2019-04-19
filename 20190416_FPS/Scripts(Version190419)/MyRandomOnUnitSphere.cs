using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRandomOnUnitSphere : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        Vector3 start = Random.onUnitSphere;
        Debug.DrawLine(start - Vector3.forward * 0.01f, start + Vector3.forward * 0.01f, Color.green, 1.0f);
        Debug.DrawLine(start - Vector3.right * 0.01f, start + Vector3.right * 0.01f, Color.green, 50.0f);
        Debug.DrawLine(start - Vector3.up * 0.01f, start + Vector3.up * 0.01f, Color.green, 50.0f);
        //Debug.Log("camera : "+ Camera.main);
    }
}
