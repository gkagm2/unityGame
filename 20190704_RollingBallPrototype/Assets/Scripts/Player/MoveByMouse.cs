using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByMouse : MonoBehaviour {
    [SerializeField]
    Transform ballPlace;

    Vector2 initialPosition;
    Vector2 mousePosition;
    float deltaX, deltaY;

    public static bool locked;


	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}

    private void OnMouseDown()
    {
        Debug.Log("MouseDown!");
        if (!locked)
        {
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("Mouse Drag!");
        if (!locked)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - deltaX, mousePosition.y - deltaY);
            Debug.Log("!!!!!!!!!!!");
        }
    }


    private void OnMouseUp()
    {
        Debug.Log("Mouse UP!");
        if (Mathf.Abs(transform.position.x - ballPlace.position.x) <= 0.5f && Mathf.Abs(transform.position.y - ballPlace.position.y) <= 0.5f)
        {
            transform.position = new Vector2(ballPlace.position.x, ballPlace.position.y);
            locked = true;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }
    //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition.x) <= 0.5f && Camera.main.ScreenToWorldPoint(Input.mousePosition.y) <= 0.5f){


    // Update is called once per frame
    void Update () {
        
	}
}
