using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProcess : MonoBehaviour {
    public GameObject groundExplosionObject;
    public GameObject airExplosioObject;

    public AudioClip clip;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float y = transform.position.y;
        if (y < 0)
            Destroy(gameObject);
	}

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision Object Name : " + collision.gameObject.name);

        int collisionLayer = collision.gameObject.layer;
        if(collisionLayer == LayerMask.NameToLayer("Ground"))
        {
            GameObject particleObj = Instantiate(groundExplosionObject) as GameObject;
            particleObj.transform.position = transform.position;
        }
        else
        {
            GameObject particleObj = Instantiate(airExplosioObject) as GameObject;
            particleObj.transform.position = transform.position;
        }

        //if(collisionLayer == LayerMask.NameToLayer("Swat"))
        //{
        //    Debug.Log("hit Swat!");
        //    GameObject particleObj = Instantiate(groundExplosionObject) as GameObject;
        //    particleObj.GetComponent<swat>().anim.enabled = false;
        //}
    
        AudioManager.Instance().PlaySfx(clip);
        Destroy(gameObject);
    }
}
