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
    
    public ModifiedStat statToEffect;
    public TypesOfInfluenceOnStat typeOfInfluence;
    [EnableIf("NotEnabled")][ShowIf("timeEffect")]
    public float startedTime;

    public enum TypesOfInfluenceOnStat
    {
        IncreaseStat,
        DecreaseStat
    }
}
