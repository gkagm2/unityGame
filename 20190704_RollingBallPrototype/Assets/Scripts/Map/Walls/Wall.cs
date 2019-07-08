using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public float speed = 10.0f; // wall speed

    // Update is called once per frame
    void Update () {
        Move();
	}



    public void Move()
    {
        transform.Translate(0, 0, -(speed * Time.deltaTime), Space.World);
    }

    // Speed setting
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


}
