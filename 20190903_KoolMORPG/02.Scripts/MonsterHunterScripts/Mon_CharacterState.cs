using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// character state class
/// </summary>
[System.Serializable]
public class Mon_CharacterState
{
    public float hp;    // health point
    public float mp;    // mana point
    public float atk;   // attack power
    public float def;   // defence power
    

    /// <summary>
    /// Set default state of character
    /// </summary>
    public void SetDefaultState()
    {
        hp = 100;
        mp = 80;
        atk = 25;
        def = 15;
    }
}
