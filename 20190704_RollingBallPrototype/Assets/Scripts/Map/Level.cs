using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Level
{
    public static LevelState currentLevel;
    // TODO : 여기서부터 한다.
    public static int levelCount = 7;
    
}

public enum LevelState
{
    level1,
    level2,
    level3,
    level4,
    level5,
    level6,
    level7
};

