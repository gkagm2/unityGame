using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public bool check;
    UISprite uiSprite;
    // Use this for initialization
    void Awake () {
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
        Debug.Log("Sprite Name : " + uiSprite.spriteName);
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
            Debug.Log("Tile" + gameObject.transform.name +"은 Tile" + collision.gameObject.name + "와 닿아있다.");
        }
    }
}
