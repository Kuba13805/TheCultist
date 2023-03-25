using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemEffect", menuName = "ScriptableObjects/ItemEffects/Create New Item Effect", order = 1)]
public class ItemEffect : ScriptableObject
{
    [SerializeField] private string effectName;
    [SerializeField] private int effectId;
    [SerializeField] private bool timeEffect;
    [SerializeField] private float effectTime;
    [SerializeField] private int pointsAffecting;
    public charStatsToEffect statToEffect;
    public typesOfInfluenceOnStat typeOfInfluence;
    public enum charStatsToEffect
    {
        Perceptivity,
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

    public enum typesOfInfluenceOnStat
    {
        IncreaseStat,
        DecreaseStat
    }

    public int AffectStat(charStatsToEffect stat, int playerDataStat)
    {
        int updatedStat = 0;
        switch (typeOfInfluence)
        {
            case typesOfInfluenceOnStat.IncreaseStat:
                updatedStat = playerDataStat + pointsAffecting;
                break;
            case typesOfInfluenceOnStat.DecreaseStat:
                updatedStat = playerDataStat - pointsAffecting;
                break;
        }

        return updatedStat;
    }
}
