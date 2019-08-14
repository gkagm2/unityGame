using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserManager {
    static public string userName;
    static public uint userKey;

    static public int score = 0;
    static public int gold = 0;
    static public int level = 1;


    public static void Save()
    {
        PlayerPrefs.SetString("UserName", userName);
    }
}
