using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    public int hp;
    public int fullhp;

    private void Start()
    {
        hp = fullhp;
    }

}
