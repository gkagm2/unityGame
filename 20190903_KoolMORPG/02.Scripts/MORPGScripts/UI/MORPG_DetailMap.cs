#define _EDITOR
//#define _MOBILE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MORPG_DetailMap : MonoBehaviour
{
    public float width;
    public float height;
    public float x;
    public float y;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        Debug.Log("widht : " + width + ", heigth : " + height);
        Debug.Log(rectTransform.rect.x +", "+ rectTransform.rect.y + ", " +
            rectTransform.rect.xMax + ", " + rectTransform.rect.xMin + ", " +
            rectTransform.rect.yMax + ", " + rectTransform.rect.yMin);
    }
    // Update is called once per frame
    void Update()
    {

#if _EDITOR
        // Editor
        if (Input.GetMouseButtonDown(0) == true) {
            Debug.Log("click 함");
        }
#elif _MOBILE
        // Mobile

        // 1. other way
        //if(Input.GetTouch(0) && Input.GetTouch(0).Phase == TouchPhase.Began){
        //}

        // 2. way
        Touch touch = Input.GetTouch(0);
        if(touch.phase == TouchPhase.Began)
        {
            Debug.Log("touch position : " + touch.position);
        }
#endif
    }   
}