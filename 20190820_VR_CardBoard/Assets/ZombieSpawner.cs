using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public GameObject zombie;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnZombie());
	}
    IEnumerator SpawnZombie()
    {
        Instantiate(zombie, transform.position, transform.rotation);
        
        yield return new WaitForSeconds(Random.Range(5,19));
        StartCoroutine(SpawnZombie());
    }
}
