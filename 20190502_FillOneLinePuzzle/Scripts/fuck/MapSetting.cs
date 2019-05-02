using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour {
    public GameObject tile;
    MapDesign mapDesign;

    public GameObject[] tiles;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}

    public void ShowMapImage()
    {
        switch (mapDesign.mapSizeState)
        {
            case MapDesign.MapSizeState.none: // 아무것도 아니면 뿌려주지 않는다.
                break;
            case MapDesign.MapSizeState.size4x4: // 4x4 사이즈 상태면
                tiles = new GameObject[4 * 4];
                for (int i = 0; i < 4 * 4; i++) //16개의 타일을 생성한다.
                {
                    tiles[i] = Instantiate(tile, gameObject.transform.position, transform.rotation) as GameObject; //
                    //tiles[i].AddComponent<UI>
                }
                break;
            case MapDesign.MapSizeState.size5x5:
                for (int i = 0; i < 5 * 5; i++)
                {
                    tiles[i] = Instantiate(tile, gameObject.transform.position, transform.rotation) as GameObject; //
                }
                break;
            case MapDesign.MapSizeState.size6x6:
                for (int i = 0; i < 6 * 6; i++)
                {
                    tiles[i] = Instantiate(tile, gameObject.transform.position, transform.rotation) as GameObject; //
                }
                break;
        }
    }
}
