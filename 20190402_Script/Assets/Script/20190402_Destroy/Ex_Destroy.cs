using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ex_Destroy : MonoBehaviour {
    public GameObject obj;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(obj, new Vector3(0, 5, 0), transform.rotation);
        Destroy(gameObject);
        return;
    }


}
