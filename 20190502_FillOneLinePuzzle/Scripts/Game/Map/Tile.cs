using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    UISprite uiSprite;
    // Use this for initialization
    void Start () {
        uiSprite = GetComponent<UISprite>();
	}
	
	// Update is called once per frame
	void Update () {

	}
    // TIle이 터치되었는지 체크한다.
    public bool CheckTouched()
    {
        return uiSprite.spriteName.Equals("tileColor2"); //tileColor2는 터치가 된 색.
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Tile")
        {
            Debug.Log("Tile" + gameObject.transform.name +"은 Tile" + collision.gameObject.name + "와 닿아있다.");
        }
    }

}
