using System.Collections;
using UnityEngine;

public class BulletControl : MonoBehaviour {

    public float speed = 20.0f;
    public float fireRange = 400.0f;
    public float damage = 10.0f;

    private Transform tr;
    private Vector3 spawnPoint;

    public GameObject hitEffect;


	// Use this for initialization
	void Start () {
        tr = this.GetComponent<Transform>();
        spawnPoint = tr.position;
	}
	
	// Update is called once per frame
	void Update () {
        tr.Translate(Vector3.forward * Time.deltaTime * speed);

        if((spawnPoint - tr.position).sqrMagnitude > fireRange)
        {
            StartCoroutine(this.DestroyBullet());
        }
	}

    IEnumerator DestroyBullet()
    {
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Instantiate(hitEffect, transform.position, transform.rotation); // 맞은 effect 생성
            Destroy(gameObject);
        }
        if (other.tag.Equals("Player"))
        {
            Instantiate(hitEffect, transform.position, transform.rotation); // 맞은 effect 생성
            Destroy(gameObject);
        }
    }
}
