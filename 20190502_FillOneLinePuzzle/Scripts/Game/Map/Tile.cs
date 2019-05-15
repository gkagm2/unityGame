using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public bool check;
    UISprite uiSprite;

    public Stack<Tile> collisionTileStack;
    // Use this for initialization
    void Awake () {
        collisionTileStack = new Stack<Tile>(); //스택 동적 할당

        uiSprite = GetComponent<UISprite>();
        check = CheckTouched();


    }
	
	// Update is called once per frame
	void Update () {
        check = CheckTouched();
	}
    // TIle이 터치되었는지 체크한다.
    public bool CheckTouched()
    {
        return uiSprite.spriteName.Equals("tileColor2"); //tileColor2는 터치가 된 색.
    }
    public void ChangeColor()
    {
        if (check) // 터치가 되었으면
        {
            uiSprite.spriteName = "tileColor1"; //터지가 안된 색으로 바꿈
        }
        else // 터치가 안되어있으면
        {
            uiSprite.spriteName = "tileColor2"; //터치가 된 색으로 바꿈.
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tile")
        {
            //충돌하는 타일이 스택에 들어있지 않으면
            if (!collisionTileStack.Contains(collision.gameObject.GetComponent<Tile>()))
            {
                //타일을 집어넣는다.
                collisionTileStack.Push(collision.gameObject.GetComponent<Tile>());
            }
        }
    }
}
