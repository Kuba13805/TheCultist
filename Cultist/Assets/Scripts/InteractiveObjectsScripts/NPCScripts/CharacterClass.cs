using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterClassData", menuName = "ScriptableObjects/CharacterClassData", order = 2)]
public class CharacterClass : ScriptableObject
{
    public string className;
    public int classId;
    
    public int difficultyToReadIntension;
    
}
