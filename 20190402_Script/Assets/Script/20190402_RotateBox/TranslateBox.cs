using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateBox : MonoBehaviour{
    public float speed = 3.0f;
    public float moveX=1.0f;
    public float moveY=1.0f;
    public float moveZ=1.0f;
    public float Limitdistance = 1.0f;
	// Use this for initialization
	void Start () {
        speed = speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(moveX * speed, moveY * speed, moveZ * speed),Space.World);

        if(transform.position.x > Limitdistance || transform.position.x < -Limitdistance)
        {
            moveX = -moveX;

        }
        if (transform.position.y > Limitdistance || transform.position.y < -Limitdistance)
        {
            moveY = -moveY;

        }
        if (transform.position.z > Limitdistance || transform.position.z < -Limitdistance)
        {
            moveZ = -moveZ;

        }
    }
}
