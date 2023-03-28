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
    [SerializeField] private int effectId;
    public bool timeEffect;
    [EnableIf("timeEffect")]
    public float effectTime;
    public int pointsAffecting;
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

    private int CalculateStatValue(int playerDataStat)
    {
        var updatedStat = playerDataStat;
        if (isEffectActive)
        {
            updatedStat = typeOfInfluence switch
            {
                typesOfInfluenceOnStat.IncreaseStat => playerDataStat + pointsAffecting,
                typesOfInfluenceOnStat.DecreaseStat => playerDataStat - pointsAffecting,
                _ => 0
            };
        }
        else if (isEffectActive != true)
        {
            updatedStat = typeOfInfluence switch
            {
                typesOfInfluenceOnStat.IncreaseStat => playerDataStat - pointsAffecting,
                typesOfInfluenceOnStat.DecreaseStat => playerDataStat + pointsAffecting,
                _ => 0
            };
        }
        return updatedStat;
    }

    public void AffectStat(PlayerData playerStats)
    {
        switch (statToEffect)
        {
            case charStatsToEffect.Perceptivity:
                playerStats.perceptivity = CalculateStatValue(playerStats.perceptivity);
                break;
            case charStatsToEffect.Occultism:
                playerStats.occultism = CalculateStatValue(playerStats.occultism);
                break;
            case charStatsToEffect.Medicine:
                playerStats.medicine = CalculateStatValue(playerStats.medicine);
                break;
            case charStatsToEffect.Electrics:
                playerStats.electrics = CalculateStatValue(playerStats.electrics);
                break;
            case charStatsToEffect.History:
                playerStats.history = CalculateStatValue(playerStats.history);
                break;
            case charStatsToEffect.Persuasion:
                playerStats.persuasion = CalculateStatValue(playerStats.persuasion);
                break;
            case charStatsToEffect.Intimidation:
                playerStats.intimidation = CalculateStatValue(playerStats.intimidation);
                break;
            case charStatsToEffect.Locksmithing:
                playerStats.locksmithing = CalculateStatValue(playerStats.locksmithing);
                break;
            case charStatsToEffect.Mechanics:
                playerStats.mechanics = CalculateStatValue(playerStats.mechanics);
                break;
            case charStatsToEffect.Psychology:
                playerStats.psychology = CalculateStatValue(playerStats.psychology);
                break;
            case charStatsToEffect.Strength:
                playerStats.strength = CalculateStatValue(playerStats.strength);
                break;
            case charStatsToEffect.Dexterity:
                playerStats.dexterity = CalculateStatValue(playerStats.dexterity);
                break;
            case charStatsToEffect.Power:
                playerStats.power = CalculateStatValue(playerStats.power);
                break;
            case charStatsToEffect.Wisdom:
                playerStats.wisdom = CalculateStatValue(playerStats.wisdom);
                break;
            case charStatsToEffect.Condition:
                playerStats.condition = CalculateStatValue(playerStats.condition);
                break;
        }
    }
}
