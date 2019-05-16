using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHit : MonoBehaviour {
    public Camera camera;
    

    public 
        
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // TODO : 마우스를 누르고 있으면 깜빡거리는 현상이 일어난다. 
        // 이를 해결하기 위해 임시로 Input.GetKeyDown으로 설정해 놓음. 고쳐야 함.
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            //GameObject map = GameObject.FindGameObjectWithTag("Map"); //Map이라는 Tag를 가진 GameObject를 가져온다.
            //Debug.Log("map name : " + map.name);

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile") //맞은 tile이면
                {
                    GameObject tileObj = hit.collider.gameObject as GameObject; // tile의 Object를 obj로 옮김
                    UISprite tileSprite = tileObj.GetComponent<UISprite>(); // UISprate 컴포넌트를 가져옴.
                    
                    Tile tile = tileObj.GetComponent<Tile>(); //타일 컴포넌트를 가져옴.

                    TileControl tileControl = tileObj.transform.parent.GetComponent<TileControl>(); // 맞은 타일 오브젝트의 tileControl을 가져옴.

                    if (tileControl.tileStack.Contains(tile)) // 누른 타일이 이미 stack에 존재하는 타일이면
                    {
                        // 눌렀던 타일 이후에 stack에 push된 타일은 stack에서 pop한다.
                        while (tileControl.tileStack.Peek() != tile) // 타일이 누른 타일과 같지 않다면
                        {//같을때까지 pop하라
                            Tile tempT = tileControl.tileStack.Pop();
                            tempT.ChangeColor();
                            //Debug.Log("pop한 타일 : " + tempT.name);
                        }
                    }

                    // 누른 타일이 현재 스택 맨 위에 있는 타일과 충돌하는 타일이면
                    if (tileControl.tileStack.Peek().collisionTileStack.Contains(tile))
                    {
                        tile.ChangeColor(); //색을 바꾼다.
                    }
                    else
                    {
                        Debug.Log("멀리 떨어져있는 타일이여서 색 못바꿈");
                    }
                }
            }
        }
	}

}
