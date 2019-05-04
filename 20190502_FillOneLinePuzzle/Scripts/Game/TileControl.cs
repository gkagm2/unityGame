using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControl : MonoBehaviour {

    public GameObject cameraObj; // 카메라 오브젝트를 가져옴 (TileHit때문임)
    TileHit tileHit; //TileHit

    int maxTileNumber = 0; // 타일의 개수
    
    Tile[] tile; //맵에 존재하는 각 타일들

    Stack<Tile> tileStack; // 타일 컴포넌트를 저장하는 Stack을 생성한다.
	// Use this for initialization
	void Start () {
        maxTileNumber = transform.childCount;
        tile = new Tile[maxTileNumber];
        for (int i=0; i < maxTileNumber; i++) //각 타일들의 Tile 컴포넌트를 가져옴.
        {
            tile[i] = transform.GetChild(i).GetComponent<Tile>();
        }

        tileHit = cameraObj.GetComponent<TileHit>(); //TileHit 컴포넌트를 가져온다.
        
        //TODO : First 첫번째 타일이 체크된것인지 확인 후 그 정보를 가져와서 Stack에 넣어야 함.
        //TEST
        //GameObject[] tileTest = new GameObject[maxTileNumber];
        //for (int i = 0; i < maxTileNumber; i++)
        //{
        //    tileTest[i] = transform.GetChild(i).gameObject;
        //}
        //for(int i = 0; i < maxTileNumber; i++)
        //{
        //    if (tileTest[i].GetComponent<Tile>().CheckTouched())
        //    {
        //        Debug.Log("첫번째 Tile을 Stack에 push 함 : " + tileTest[i].name);

        //    }
        //}


        //for (int i = 0; i < maxTileNumber; i++) // 첫 번째 타일의 정보를 찾아서 스택에 Push한다.
        //{
        //    Debug.Log(" 찾는중 ");
        //    if (tile[i]) //터치하면
        //    {
        //        Debug.Log("첫번째 Tile을 Stack에 push 함 : " + tile[i].name);
        //        tileStack.Push(tile[i]); //그 타일의 정보를 스택에 넣고
        //        break; //중지
        //    }
        //}
        //Debug.Log(tileStack.Peek());

    }
	
	// Update is called once per frame
	void Update () {
        if (CheckAllTilesTouched()) //모든 타일들이 터치되면
        {
            Debug.Log("All tiles Touched");
            // TODO :  Show Success Pop up Screen
        }
    }
    
    // 모든 타일들이 터치되었는지 확인하는 함수.
    public bool CheckAllTilesTouched()    {
        int touchCount = 0;
        for(int i = 0; i < maxTileNumber; i++)
        {
            if (tile[i].CheckTouched()) //터치되었으면 
            {
                ++touchCount; //터치한 개수를 올림.
            }
        }
        return touchCount == maxTileNumber; //터치한 개수와 최대 타일 개수와 같으면 true
    }

    // 스택에 타일을 저장함.
    public void PushTileInStack(Tile tile)
    {
        tileStack.Push(tile);
    }



}
