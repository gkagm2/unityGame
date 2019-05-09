using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHit : MonoBehaviour {
    public Camera camera;
    
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
                    
                    if (!tile.CheckTouched()) //타일이 터치가 되어있지 않으면
                    {
                        // TODO : 여기부터 해야 함.

                        tileSprite.spriteName = "tileColor2"; // 색상을 터치한 색으로 바꿈.

                    }
                    else //타일이 터치가 되어있으면
                    {
                        tileSprite.spriteName = "tileColor1"; // 색상을 터치안한 색으로 바꿈.
                    }

                }
            }
        }
	}

}
