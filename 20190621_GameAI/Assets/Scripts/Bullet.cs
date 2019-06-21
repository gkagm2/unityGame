using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float bulletSpeed = 10f;
    public GameObject hitParticle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, bulletSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enemy 맞춤");

            Instantiate(hitParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<AIController>().HitByBullet();
            Debug.Log("Enemy 맞춤");
            Instantiate(hitParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
