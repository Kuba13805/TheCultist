using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PlayerScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemEffect", menuName = "ScriptableObjects/ItemEffects/Create New Item Effect", order = 1)]
public class ItemEffect : ScriptableObject
{
    public bool isEffectActive;
    public string effectName;
    public int effectId;
    public bool timeEffect;
    [EnableIf("timeEffect")]
    public float effectTime;
    public int pointsAffecting;
    
    public CharStatsToEffect statToEffect;
    public TypesOfInfluenceOnStat typeOfInfluence;
    [EnableIf("NotEnabled")][ShowIf("timeEffect")]
    public float startedTime;

    public enum CharStatsToEffect
    {
        Perception,
        Occultism,
        Medicine,
        Electrics,
        History,
        Persuasion,
        Intimidation,
        Locksmithing,
        Mechanics,
        Psychology,
        Strength,
        Dexterity,
        Power,
        Wisdom,
        Condition
    }

    public enum TypesOfInfluenceOnStat
    {
        IncreaseStat,
        DecreaseStat
    }
}
