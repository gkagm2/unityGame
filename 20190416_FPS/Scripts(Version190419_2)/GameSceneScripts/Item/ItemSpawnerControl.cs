using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerControl : MonoBehaviour {

    // Prefabs
    public GameObject[] itemObj;

    // Parameters
    float randomDistanceX = 100.0f; // random value
    float randomDistanceZ = 100.0f; // random value
    float randomRespawnTime = 10f;        // max respawn time
    float tRandomRespawnTime;       // respawn time (temp)


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float distanceX = Random.Range(-randomDistanceX, randomDistanceX);
        float distanceZ = Random.Range(-randomDistanceZ, randomDistanceZ);
        Vector3 randomPosition = new Vector3(distanceX, transform.position.y, distanceZ); //랜덤 위치

        tRandomRespawnTime -= Time.deltaTime;
        if(tRandomRespawnTime <= 0) { //리스폰타임시간이 끝나면
            tRandomRespawnTime = randomRespawnTime;
            //Debug.Log("Item Spawn!!");
            bool flag = true;
            for(int i=0;i < itemObj.Length; i++)
            {
                if (itemObj[i].activeSelf == true) //하나라도  active true면
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                transform.position = randomPosition;
                int randomIndex = Random.Range(0, itemObj.Length);
                itemObj[randomIndex].SetActive(true);
            }
            

        }

	}
}
