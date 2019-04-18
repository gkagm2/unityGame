using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    int damage = 25;
    GameManager gameManager;
    public float moveAmt;
    [Header("Enemy control")]
    public float worstSpeed = 3.0f;
    public float festSpeed = 5.0f;
    public int hp = 100;

    public float distanceLimit = -3.0f;

    //소리
    //public GameObject sound;

    [Header("Sound")]
    //sound
    public AudioClip explosionClip;
    public AudioSource explosionSource;
       

    void Start()
    {
        //GameMrg라는 이름의 오브젝트에 GameManager의 (class)이름을 가진 컴포넌트를 gameManager로 가져온다
        gameManager = GameObject.Find("GameMrg").GetComponent<GameManager>();
        moveAmt = Random.Range(worstSpeed, festSpeed) * Time.deltaTime;

        explosionSource.clip = explosionClip;
    }

    void Update () {
       
        //맵에서 벗어나면 없앤다.
        if (transform.position.y < distanceLimit)
        {
            Destroy(gameObject);
        }

        //아래로 이동
        transform.Translate(Vector3.down * moveAmt);

        //hp가 0이면 죽는다..
        if(hp <= 0)
        {
            //소리
            //Instantiate(sound, transform.position, transform.rotation);
            Debug.Log(GetComponent<AudioSource>());
            //왜 안먹히지
            //GetComponent<AudioSource>().Play(); //죽을 때 폭발음 나옴.

            explosionSource.Play();
            gameManager.PlusScore(3); // 3점 추가
            Destroy(gameObject);
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        //총알과 충돌 했을 경우
        if(collision.gameObject.tag == "Bullet")
        {
            //점수를 올린다.
            hp -= damage;
        }
    }
}
