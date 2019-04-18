using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

    public GameObject touchEffect;
    //GameManager gameManager;
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {        
            Instantiate(touchEffect, transform.position, transform.rotation);
        }
    }
}
