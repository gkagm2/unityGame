using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MORPG_EnumyRespawnArea : MonoBehaviour
{
    [Header("속성값")]
    [Tooltip("생성 범위")]
    [Range(0,999)]
    public float maxRespawnRange = 20.0f;

    [Header("적 오브젝트")]
    public GameObject[] enemys;
    private Transform[] areaTransform;

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