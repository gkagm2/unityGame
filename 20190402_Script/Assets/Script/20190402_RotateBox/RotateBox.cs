using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox : MonoBehaviour {
    public float xRot;
    public float yRot;
    public float zRot;
    public float Limitdistance = 1.0f;
	// Use this for initialization
	void Start () {

        xRot = xRot * Time.deltaTime;
        yRot = yRot * Time.deltaTime;
        zRot = zRot * Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x > Limitdistance || transform.position.x < -Limitdistance)
        {
            xRot = -xRot;
        }
        if(transform.position.y > Limitdistance || transform.position.y < -Limitdistance)
        {
            yRot = -yRot;
        }if(transform.position.z > Limitdistance || transform.position.z < -Limitdistance)
        {
            zRot = -zRot;
        }
        
        transform.Rotate(Random.Range(0, 120) * xRot, Random.Range(0,120) * yRot, Random.Range(0, 120) *zRot);
	}
}
