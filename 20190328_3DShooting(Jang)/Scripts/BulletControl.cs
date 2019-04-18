using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour {
    public float Speed = 20.0f;
    private bool hit;
    public int bulletRange = 10;
	// Use this for initialization
	void Start () {
        hit = false;
	}
	
	// Update is called once per frame
	void Update () {
        float moveAmt = Time.deltaTime * Speed;
        transform.Translate(Vector3.right * moveAmt);
        if(transform.position.y > bulletRange)
        {
            Destroy(gameObject);
        }
        if (hit == true)
        {
            Destroy(gameObject);
        }
        
	}
    void OnTriggerEnter(Collider other)
    {
        hit = true;
    }
}
