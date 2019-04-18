using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float Speed = 1.0f;
    public GameObject bullet;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveAmt = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        //float moveAmt = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        //transform.Translate(Vector3.right * moveAmt);
        transform.Translate(Vector3.back * moveAmt);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //audio.Play(); <- 이거 안됨
            GetComponent<AudioSource>().Play();

            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
