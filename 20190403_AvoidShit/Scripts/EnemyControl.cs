using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    
    public int passedEnemyCount; //Player를 통과한 Enemy 개수

    [Header("Explosion Object")]

    public GameObject explosionObj;

    //// 충돌한 순간
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
    //    {
    //        print(collision.gameObject.tag + " 에 닿음");
    //    }
    //}

    //// 총돌 통과 순간
    private void OnTriggerEnter(Collider other)
    {
        // Enemy가 Ground라는 태그를 가진 Object를 충돌 통과하는 순간
        if (other.gameObject.tag == "Ground")
        {
            /// TODO : 폭발음 추가
            ++passedEnemyCount; // Player를 통과한 Enemy의 개수를 1 증가
            Instantiate(explosionObj, transform.position, transform.rotation);//폭발 오브젝트 생성 후
            Destroy(gameObject);//Enemy 오브젝트를 없앰.
        }
    }

    private void Update()
    {
        //밑으로 떨어진 Enymy가 끝까지 내려가면
        if (transform.position.y < -1)
            Destroy(gameObject);
    }
    
}
