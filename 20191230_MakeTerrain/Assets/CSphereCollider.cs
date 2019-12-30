using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSphereCollider : MonoBehaviour
{
    public float fScale =5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, fScale);
    }
}
