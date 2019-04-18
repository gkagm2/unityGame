using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public enum fireType { NOMAL, HEAVY, FAST };
    fireType currentFireType = fireType.NOMAL;
    [Header("bullet control")]
    public float speed = 15.0f;

	// Update is called once per frame
	void Update () {
        float moveAmt = speed * Time.deltaTime;
        switch (currentFireType)
        {
            case fireType.NOMAL:
                //움직임
                transform.Translate(0, moveAmt, 0);
                break;

            case fireType.HEAVY:
                transform.Translate(0, moveAmt, 0);
                break;

            case fireType.FAST:
                transform.Translate(0, moveAmt, 0.0f);
                break;
        }
        

        //2초 후에 없어짐.
        Destroy(gameObject, 2);
    }
    void OnCollisionEnter(Collision collision)
    {
        //Enemy를 맞추면
        if(collision.gameObject.tag == "Enemy")
        {
            //총알 없앤다.
            Destroy(gameObject);
        }
    }
}
