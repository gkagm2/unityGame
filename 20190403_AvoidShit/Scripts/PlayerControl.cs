using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static bool alive = true;
    public GameObject explosionObj;
    public float speed = 3.0f;
    public float movementDistance = 4.0f;

    // Update is called once per frame
    void Update()
    {
        float moveAmt = Time.deltaTime * speed;

        //살아있으면
        if (alive)
        {
            //왼쪽 오른쪽 움직인다
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.right * moveAmt);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.left * moveAmt);
            }

            //왼쪽과 오른쪽 끝까지 움직일 수 있는 범위
            if (transform.position.x > movementDistance)
            {
                transform.Translate(Vector3.left * moveAmt);

            }
            if (transform.position.x < -movementDistance)
            {
                transform.Translate(Vector3.right * moveAmt);
            }
        }
    }
    //충돌 통과시
    private void OnTriggerEnter(Collider other)
    {
        //플레이어와 태그 이름이 Enemy인 오브젝트와 충돌 통과 시
        if(other.gameObject.tag == "Enemy")
        {
            
            alive = false; //플래그는 죽는걸로 바꿈
            Instantiate(explosionObj, transform.position, transform.rotation); // 터지는 효과 나타내고
            Destroy(gameObject); // 플레이어 오브젝트 없앰
        }
    }
}
