using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_EnumyRespawnArea : MonoBehaviour
{
    private Transform[] areaTransform;

    public GameObject[] enemys;

    private float maxRespawnRange =20.0f;

    private void Start()
    {
        for(int i=0; i < enemys.Length; ++i)
        {
            float xRange = transform.position.x + Random.Range(-maxRespawnRange, maxRespawnRange);
            float zRange = transform.position.z + Random.Range(-maxRespawnRange, maxRespawnRange);

            // generate enemys
            Instantiate(enemys[i], new Vector3(xRange, 0, zRange), Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f))));
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxRespawnRange);
    }
}   