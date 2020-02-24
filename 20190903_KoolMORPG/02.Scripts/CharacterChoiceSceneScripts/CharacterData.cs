using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultCharacterData", menuName = "CharacterData")]

public class CharacterData : ScriptableObject
{
    public string characterType;
    public string characterDescription;
}
