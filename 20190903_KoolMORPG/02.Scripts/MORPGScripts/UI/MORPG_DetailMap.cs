#define _EDITOR
//#define _MOBILE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MORPG_DetailMap : MonoBehaviour
{
    public Camera cam;
    public MORPG_PlayerMotor playerMotor;


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
    /// 맵 이미지를 눌렀으면 호출된다.
    /// </summary>
    public void OnClick_DetailMap()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("맵 이미지의 사이즈 : " + rectTransform.rect.size);
        // 400 , 400 나온다.

        // 이미지의 RectTransform 컴포넌트를 가져와 좌표를 구한다.
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("왼쪽 하단 모서리의 스크린 좌표: " + rectTransform.offsetMin);
        
        // 마우스를 클릭한 스크린 좌표값을 가져온다.
        Vector2 mouseClickedPosition = Input.mousePosition;
        Debug.Log("마우스를 클릭한 스크린 좌표: " + mouseClickedPosition);
        
        // 클릭한 픽셀 좌표값에서 이미지 좌, 하단의 좌표값을 빼 값을 구한다.
        Vector2 clickedPosition = mouseClickedPosition - rectTransform.offsetMin;

        // UI이미지의 가로와 세로를 각각 나눠서 비율을 계산한다.
        Vector2 ratioVec = clickedPosition / rectTransform.rect.size;

        // 월드맵 상의 좌표값으로 변환해준다.
        Vector3 worldMapPosition;
        worldMapPosition.x = ratioVec.x * 400; // 400 -> Plane의 width
        worldMapPosition.z = ratioVec.y * 400; // 400 -> Plane의 height
        worldMapPosition.y = 0;                // 높이는 0임.

        playerMotor.MoveToPoint(worldMapPosition); // 플레이어는 변경된 좌표값으로 이동한다.
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