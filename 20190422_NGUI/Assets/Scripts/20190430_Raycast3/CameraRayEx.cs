using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayEx : MonoBehaviour {
    public GameObject box;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //I
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
                if(hit.collider.name.Equals("VioletZone")) // 보라색이 면
                {
                    Debug.Log("violet zone !!!!");
                    GameObject obj = Instantiate(box) as GameObject;
                    obj.transform.position = hit.transform.position + Vector3.up;
                    obj.transform.rotation = hit.transform.rotation;
                }
            }

        }
	}
}
