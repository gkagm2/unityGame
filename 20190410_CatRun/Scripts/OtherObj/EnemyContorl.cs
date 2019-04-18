using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContorl : MonoBehaviour {

    public GameObject touchEffect;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    void Update()
    {
        //게임 오버면
        if (gameManager.gameOver)
        {
            Instantiate(touchEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(touchEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(gameObject.name == "Enemy2")
        {
            Debug.Log("name :" + gameObject.name);
        }
        if (gameObject.name=="Enemy2" && collision.gameObject.tag == "Shield")
        {
            Debug.Log("수박 박살!");
            Instantiate(touchEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
