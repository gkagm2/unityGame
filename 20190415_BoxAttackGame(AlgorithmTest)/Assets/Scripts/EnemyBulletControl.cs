using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletControl : MonoBehaviour {
    public float speed = 30.0f;

    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update () {
        float moveAmt = speed * Time.deltaTime;
        BulletMove(moveAmt);
    }

    void BulletMove(float moveAmt)
    {
        transform.Translate(0.0f, -moveAmt, 0.0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
