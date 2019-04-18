using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_Instantiate : MonoBehaviour {
    public GameObject firstObj;
    public GameObject secondObj;
    private void OnCollisionEnter(Collision collision)
    {

        //Instantiate(gameObject);
        //Instantiate(gameObject, new Vector3(0, 5, 0), transform.rotation);

        if (collision.transform.name == "Ground")
        {
            Destroy(gameObject);
            Instantiate(secondObj, new Vector3(0, 5, 0), transform.rotation);
            return;

        }

    }
    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(gameObject, new Vector3(3, 5, 3), transform.rotation);
    }
}
