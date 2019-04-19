using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour {

    //Bomb
    public Transform cameraTransform;
    public GameObject fireObject;
    public Transform firePoint;
    public float power = 1000.0f;

    //플레이어 죽음 상태 처리
    PlayerState playerHealth = null;

    // Prefabs
    public GameObject bomb;
    public GameObject muzzleFlash; //Muzzle
    public GameObject gunHoldEffect; //총알 구멍
    public GameObject gunHoldTexture; //총알구멍 자국

    // Sound
    public AudioClip gunshootSoundClip;
    public AudioClip [] bulletBounceSound;



    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerState>();
    }
    void Update () {
        if (playerHealth.isDead)
            return;
        if (Input.GetButtonDown("Fire1")) // Mouse0 마우스를 누르면
        {
            AudioManager.Instance().PlaySfx(gunshootSoundClip); //발사 소리

            // 총구 반짝임 시작
            GameObject muzzle = Instantiate(muzzleFlash, transform.position, transform.rotation) as GameObject;
            Destroy(muzzle,2f);


            //Raycast 사용하기
            float maxDistance = 100;
            RaycastHit hit;
            bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance);
            Gizmos.color = Color.yellow;
            if (isHit)
            {

                if (hit.transform.gameObject.tag == "Enemy") //Enemy를 맞췄으면
                {
                    HitEnemy(hit); //적을 맞춤
                }
                else //다른곳을 맞추면
                {
                    AudioManager.Instance().PlaySfx(bulletBounceSound[Random.Range(0, 3)]);

                    GameObject gunholdEffectTemp = Instantiate(gunHoldEffect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
                    Destroy(gunholdEffectTemp, 3f);
                    GameObject gunhold = Instantiate(gunHoldTexture, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
                    Destroy(gunhold, 10.0f);
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            AudioManager.Instance().PlaySfx(gunshootSoundClip); //발사소리


            // 총구 반짝임 시작
            GameObject muzzle = Instantiate(muzzleFlash, transform.position, transform.rotation) as GameObject;
            Destroy(muzzle, 2f);


            GameObject obj = Instantiate(fireObject) as GameObject; //fireObject를 생성하고 GameObject로 형변환하여 obj에 넣는다.
            obj.transform.position = transform.position;
            obj.GetComponent<Rigidbody>().velocity = cameraTransform.forward * power; //카메라 앞쪽 방향으로 힘을 준다.
        }
    }

    //Enemy를 맞췄을 때 
    public void HitEnemy(RaycastHit hit)
    {
        EnemyScript enemy = hit.transform.gameObject.GetComponent<EnemyScript>(); //상대방의 EnemyScript컴포넌트를 가져온다.

        if (enemy.enemyState == EnemyScript.EnemyState.dead) //죽은 상태면 return
        {
            Debug.Log("죽은 상태");
            return;
        }
        Debug.Log("살아있는 상태");

        Debug.Log("Enemy hp :  "+ enemy.hp);

        enemy.ApplyHitEffect(hit);
        enemy.enemyState = EnemyScript.EnemyState.damage; //Enemy의 상태를 damage 로 바꾼다.
 
        // TODO : 이게 뭔가?!
        //int layerIndex = enemy.gameObject.layer;
        //if (layerIndex != 10)                                          //이것도 같다.
        //    return;
    }


}
