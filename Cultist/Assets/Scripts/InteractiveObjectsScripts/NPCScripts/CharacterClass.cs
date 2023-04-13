using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterClassData", menuName = "ScriptableObjects/ Create New Character Class", order = 2)]
public class CharacterClass : ScriptableObject
{
    public string className;
    public int classId;
    
    public int difficultyToReadIntension;
    public bool canBeIntimidated;
    public bool canBePersuaded;

    public Sprite classPortrait;
    [SerializeField] private List<BaseItem> itemsToSteal;

    private int dexterity;
    private int strength;
    private int power;
    private int condition;
    private int wisdom;

    private int health;
}
