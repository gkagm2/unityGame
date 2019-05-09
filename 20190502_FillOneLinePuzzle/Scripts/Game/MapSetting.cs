using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour {
    public UIGrid uiGrid;
    public GameObject tile;
    MapDesign mapDesign;

    public GameObject[] tiles;
	// Use this for initialization
	void Start () {
        uiGrid.enabled = true; //Active true
        mapDesign = new MapDesign();
        Debug.Log("MapSetting Start!");
        
        if (gameObject.name.Equals("Map4x4"))
        {
            Debug.Log("Map4x4 ");
            mapDesign.mapSizeState = MapDesign.MapSizeState.size4x4;
        }
        if (gameObject.name.Equals("Map5x5"))
        {
            Debug.Log("Map5x5 ");
            mapDesign.mapSizeState = MapDesign.MapSizeState.size5x5;
        }
        if (gameObject.name.Equals("Map6x6"))
        {
            Debug.Log("Map5x5 ");
            mapDesign.mapSizeState = MapDesign.MapSizeState.size6x6;
        }
        MakeMapImage();

    }
	
	// Update is called once per frame
	void Update () {
        
        
	}

    public void MakeMapImage()
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
                    tiles[i].transform.parent = gameObject.transform;
                    tiles[i].transform.localScale = Vector3.one;
                    
                }
                Debug.Log("4x4 사이즈 타일 생성 완료");
                break;
            case MapDesign.MapSizeState.size5x5:
                for (int i = 0; i < 5 * 5; i++)
                {
                    tiles[i] = Instantiate(tile, gameObject.transform.position, transform.rotation) as GameObject; //
                    tiles[i].transform.parent = gameObject.transform;
                    tiles[i].transform.localScale = Vector3.one;
                }
                Debug.Log("5x5 사이즈 타일 생성 완료");
                break;
            case MapDesign.MapSizeState.size6x6:
                for (int i = 0; i < 6 * 6; i++)
                {
                    tiles[i] = Instantiate(tile, gameObject.transform.position, transform.rotation) as GameObject; //
                    tiles[i].transform.parent = gameObject.transform;
                    tiles[i].transform.localScale = Vector3.one;
                }
                Debug.Log("6x6 사이즈 타일 생성 완료");
                break;
        }
    }
}
