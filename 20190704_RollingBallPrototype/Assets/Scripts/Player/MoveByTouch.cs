using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour {
    public GameObject targetObj;
    public Camera camera;


    float b = 0;
    float c = 0;
    // Update is called once per frame
    void Update () {
        Touch3();
	}

    // screen on mobile 
    public void Touch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // first touch : 0
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            transform.position = touchPosition;
            Debug.Log("deltaPosition : " + touch.deltaPosition);
        }

        
    }
    // screen on mobile
    public void Touch2()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            Debug.DrawLine(Vector3.zero, touchPosition, Color.red);
        }
    }

    public void Touch3()
    {
        

        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position; // 터치한 위치
            Vector3 theTouch = new Vector3(pos.x, pos.y, 0f);


            float sensitivity = 20f;

            Ray ray = camera.ScreenPointToRay(theTouch);
            RaycastHit hit;

            // TODO : 불안정 하다.
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began) // 처음 터치 할때 발생
                {
                    b = hit.point.x;
                    //Debug.Log("처음 터치 " + hit.point);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Moved) // 터치하고 움직이면 발생
                {
                    //Debug.Log("터치하고 움직임 " + hit.point);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended) // 터치를 떼면 발생
                {
                    b = 0;
                    //Debug.Log("터치 뗌 " + hit.point);
                }
                if (b > hit.point.x)
                {
                    sensitivity *= -1;
                }
                else
                { 
                    
                }
                Debug.Log("----------" + Mathf.Abs(hit.point.x - b));
                
                targetObj.transform.Rotate(0, 0, sensitivity * Mathf.Abs(hit.point.x - b));
                b = hit.point.x;
                Debug.Log("b : " + b);
            }
        }
    }
}
