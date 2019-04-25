using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public int hp;
    public int fullhp;
    private void Start()
    {
        hp = fullhp;
    }
}
