using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_Collision : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        print("collide!");
        //transform.Translate(new Vector3(0, 5, 0));
        transform.Translate(new Vector3(0, 5,0));

        //collision은 누구꺼? : 충돌한 대상
        print("collision.transform.name : " + collision.transform.name);
        //collision의 tag는? 
        print("collision.transform.tag : " + collision.transform.tag);
        print("this gameObject.name : " + gameObject.name);
        print("this gameObject.tag : " + gameObject.tag);
    }

}
