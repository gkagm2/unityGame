using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 6.0f;
        Destroy(gameObject, 2.0f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 상대방에게 데미지를 주기

        Destroy(gameObject);
    }
}
