using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObjects/Create New Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    //Podstawowe informacje o postaci
    public string name;
    public string nickname;
    public float health;
    
    [Foldout("Attributes")]
    public int dexterity;
    [Foldout("Attributes")]
    public int strength;
    [Foldout("Attributes")]
    public int power;
    [Foldout("Attributes")]
    public int condition;
    [Foldout("Attributes")]
    public int wisdom;

    [Foldout("Skills")]
    public int perceptivity;
    [Foldout("Skills")]
    public int occultism;
    [Foldout("Skills")]
    public int medicine;
    [Foldout("Skills")]
    public int electrics;
    [Foldout("Skills")]
    public int history;
    [Foldout("Skills")]
    public int persuasion;
    [Foldout("Skills")]
    public int intimidation;
    [Foldout("Skills")]
    public int locksmithing;
    [Foldout("Skills")]
    public int mechanics;
    [Foldout("Skills")]
    public int psychology;

    public string lastLocation;

    public List<BaseItem> playerInventoryItems;

    public List<BaseItem> characterEquipment;
}
