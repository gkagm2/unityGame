using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprite : SpriteBase {
    public float Speed = 10.0f;
    public GameObject explosion;

	void Start () {
        gameObject.AddComponent<BoxCollider>();
	}
	
	void Update () {
        float moveAmount = Speed * Time.deltaTime;
        transform.Translate(Vector3.down * moveAmount);
        if(transform.position.y < -3.0f)
        {
            ResetPosition();
        }
	}
    void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 2.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Instantiate(explosion.transform, transform.position, transform.rotation);
            ResetPosition();
        }
    }

}
