using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDesign {
    public GameObject[] maps;
    public enum MapSizeState
    {
        none,       // 아무것도 없음
        size4x4,    // 4 x 4 사이즈
        size5x5,    // 5 x 5 사이즈
        size6x6     // 6 x 6 사이즈
    }
    public MapSizeState mapSizeState;
}
