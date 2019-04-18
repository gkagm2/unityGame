using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class My_Input : MonoBehaviour {
    public float speed = 3.0f;
    public Camera cam;
    public Transform target;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        float moveAmt = speed * Time.deltaTime;
        //// 정확하게 움직인다.
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Translate(Vector3.left * moveAmt);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(Vector3.right * moveAmt);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    transform.Translate(Vector3.forward * moveAmt);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    transform.Translate(Vector3.back * moveAmt);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mos = Input.mousePosition;
            mos.z = cam.farClipPlane; // 카메라가 보는 방향과, 시야를 가져온다.
            Debug.Log("mos : " + mos);

            Vector3 dir = cam.ScreenToWorldPoint(mos);
            // 월드의 좌표를 클릭했을 때 화면에 자신이 보고있는 화면에 맞춰 좌표를 바꿔준다.

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, mos.z))
            {
                target.position = hit.point; // 타겟을 레이캐스트가 충돌된 곳으로 옮긴다.
                Debug.Log(hit.point);
                target.TransformVector(target.position);

            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

        }

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
        

        // 부드럽게 움직인다.
        float vertical = Input.GetAxis("Vertical") * moveAmt;
        float horizontal = Input.GetAxis("Horizontal") * moveAmt;
        transform.Translate(0, 0, vertical);
        transform.Translate(horizontal, 0, 0);
        
        cam.transform.Translate(horizontal, vertical, 0);


    }
}
