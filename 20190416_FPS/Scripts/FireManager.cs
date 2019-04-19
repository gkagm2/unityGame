using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour {
    public Transform cameraTransform;
    public GameObject fireObject;
    public Transform firePoint;
    public float power = 1000.0f;

    public GameObject hold;

    //Muzzle
    public ParticleSystem muzzleFlash;

    //플레이어 죽음 상태 처리
    PlayerState playerHealth = null;


    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerState>();
    }
    void Update () {
        if (playerHealth.isDead)
            return;
        if (Input.GetButtonDown("Fire1")) // Mouse0 마우스를 누르면
        {

            //Raycast 사용하기
            float maxDistance = 100;
            RaycastHit hit;
            bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance);
            Gizmos.color = Color.yellow;
            if (isHit)
            {
                
                if(hit.transform.gameObject.tag == "Enemy")
                {
                    HitEnemy(hit); //적을 맞춤
                }
            }

            // 총구 반짝임 시작
            muzzleFlash.Play();

            //GameObject obj = Instantiate(fireObject) as GameObject; //fireObject를 생성하고 GameObject로 형변환하여 obj에 넣는다.
            //obj.transform.position = transform.position;
            ////obj.transform.position = transform.position;
            ////obj.transform.position = firePoint.transform.position; //firePoint위치로 바꿈
            //obj.GetComponent<Rigidbody>().velocity = cameraTransform.forward * power;
        }
	}
    public void HitEnemy(RaycastHit hit)
    {
        Debug.Log("HitEnemy stsart");
        //Destroy(hit.transform.gameObject);
        EnemyScript enemy = hit.transform.gameObject.GetComponent<EnemyScript>();
        if (enemy.enemyState == EnemyScript.EnemyState.dead)
            return;
        
        Instantiate(hold, transform.position - hit.transform.position + Vector3.forward * hit.distance, transform.localRotation);
        Debug.Log("꺼내옴 : ");
        enemy.enemyState = EnemyScript.EnemyState.damage;
        int layerIndex = enemy.gameObject.layer;
        if (layerIndex != 10)                                          //이것도 같다.
            return;



    }


}
