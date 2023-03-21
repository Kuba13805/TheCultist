using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    //Podstawowe informacje o postaci
    public string name;
    public string nickname;
    public float health;
    //Cechy
    public int dexterity;
    public int strength;
    public int power;
    public int condition;
    public int wisdom;
    //Umiejętności
    public int perceptivity;
    public int occultism;
    public int medicine;
    public int electrics;
    public int history;
    public int persuasion;
    public int intimidation;
    public int locksmithing;
    public int mechanics;
    public int psychology;

    public string lastLocation;
}
