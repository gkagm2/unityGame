#define _EDITOR
//#define _MOBILE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MORPG_DetailMap : MonoBehaviour
{
    public Camera cam;
    private float width;
    private float height;
    private float x;
    private float y;
    private RectTransform rectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// 맵을 눌렀으면 호출된다.
    /// </summary>
    public void OnClick_DetailMap()
    {
        rectTransform = GetComponent<RectTransform>();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;
        Debug.Log("widht : " + width + ", heigth : " + height);
        Debug.Log(rectTransform.rect.x + ", " + rectTransform.rect.y + ", " +
            rectTransform.rect.xMax + ", " + rectTransform.rect.xMin + ", " +
            rectTransform.rect.yMax + ", " + rectTransform.rect.yMin);


    }

    // Update is called once per frame
    void Update()
    {
#if _EDITOR
        // Editor
        if (Input.GetMouseButtonDown(0) == true)
        {
            Debug.Log("마우스 포지션 값을 가져온다. : " + Input.mousePosition);
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

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + (point.x * 40) + "," + (point.z * 40));
        GUILayout.EndArea();
    }
}