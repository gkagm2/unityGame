using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpearControl : MonoBehaviour {

    public GameObject hitEffect;

    public ParticleSystem ptc;
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
