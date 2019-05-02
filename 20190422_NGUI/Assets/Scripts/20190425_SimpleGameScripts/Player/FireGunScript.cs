using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGunScript : MonoBehaviour {
    public GameObject bullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void Fire()
    {
        GameObject bulletObj = Instantiate(bullet) as GameObject;
        bulletObj.transform.position = transform.position;
        bulletObj.transform.rotation = transform.rotation;
        Destroy(bulletObj, 5f);
    }
}
