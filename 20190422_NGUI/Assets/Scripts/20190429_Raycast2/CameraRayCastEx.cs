using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCastEx : MonoBehaviour {
    public GameObject bullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit!!" + hit.transform.name);
                Instantiate(bullet, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
	}
}
