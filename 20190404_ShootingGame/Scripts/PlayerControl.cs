using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    int damage = 25;
    GameManager gameManager;

    [Header("Explosion effect")]
    public GameObject explosionEffect;

    //public GameObject explosionObj;
    [Header("Player control")]
    public float speed = 3.0f;
    public float movementDistanceX = 4.0f;
    public float hp = 100;



    
    void Start()
    {
        gameManager = GameObject.Find("GameMrg").GetComponent<GameManager>();
        gameManager.playerHpText.text = hp.ToString(); //플레이어의 hp를 UI로 보님
    }

    void Update () {
        float moveAmt = Time.deltaTime * speed;

        //살아있으면
        if (!gameManager.IsGameOver())
        {
            //왼쪽 오른쪽 
            //float di = Input.GetAxis("Horizontal");
            //transform.Translate(new Vector3(di * moveAmt, 0.0f, 0.0f));

            //뻣뻣하게 하려면
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * moveAmt);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * moveAmt);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector3.up * moveAmt);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(Vector3.down * moveAmt);
            }

            //왼쪽과 오른쪽, 위, 아래 끝까지 움직일 수 있는 범위
            if (transform.position.x > movementDistanceX)
            {
                transform.Translate(Vector3.left * moveAmt);

            }
            if (transform.position.x < -movementDistanceX)
            {
                transform.Translate(Vector3.right * moveAmt);
            }
            if(transform.position.y > 9.13f)
            {
                transform.Translate(Vector3.down * moveAmt);
            }
            if(transform.position.y < 0)
            {
                transform.Translate(Vector3.up * moveAmt);
            }
        }

        gameManager.playerHpText.text = hp.ToString();
        if (hp <= 0) //hp가 다 깎이면
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            gameManager.GameOver = true;
            Destroy(gameObject); //Player를 없앰.
        }
    }
    //private void OnTriggerEnter(Collider collider)
    //{
    //    //Enemy와 닿으면 없어짐.
    //    if(collider.gameObject.tag == "Enemy")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    
    private void OnCollisionEnter(Collision collision)
    {
        //Enemy와 충돌하면
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            hp -= damage;
        }
    }
}
