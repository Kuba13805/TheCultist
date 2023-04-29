using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using NaughtyAttributes;
using PlayerScripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoadCharacterInfo : MonoBehaviour
{
    private TextMeshProUGUI inputToLoad;
    private PlayerData PlayerData;
    [DisableIf(EConditionOperator.Or, "LoadBasic", "LoadSkill")] 
    public bool LoadAttribute;
    [DisableIf(EConditionOperator.Or, "LoadAttribute", "LoadSkill")] 
    public bool LoadBasic;
    [DisableIf(EConditionOperator.Or, "LoadBasic", "LoadAttribute")] 
    public bool LoadSkill;
    [EnableIf("LoadAttribute")] [ShowIf("LoadAttribute")]
    public charAttributes attributeToLoad;
    
    [EnableIf("LoadBasic")] [ShowIf("LoadBasic")]
    public charBasicInfo basicInfoToLoad;
    
    [EnableIf("LoadSkill")] [ShowIf("LoadSkill")]
    public charSkills skillToLoad;
    public enum charAttributes
    {
        Strength,
        Dexterity,
        Power,
        Wisdom,
        Condition
    }

    public enum charBasicInfo
    {
        Name,
        Health
    }
    public enum charSkills
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
        Psychology
    }

    private void OnEnable()
    {
        InventoryItemDragDrop.OnItemChanged += SwitchOnOff;
        
        inputToLoad = GetComponent<TextMeshProUGUI>();
        PlayerData = GameManager.Instance.playerData;
        LoadData();
        
    }

    private void OnDisable()
    {
        InventoryItemDragDrop.OnItemChanged -= SwitchOnOff;
    }

    private void LoadData()
    {
        if (LoadAttribute)
        {
            LoadCharAttribute(attributeToLoad);
        }
        else if (LoadBasic)
        {
            LoadCharBasicInfo(basicInfoToLoad);
        }
        else if (LoadSkill)
        {
            LoadCharSkill(skillToLoad);
        }
    }

    private void LoadCharAttribute(charAttributes attribute)
    {
        inputToLoad.text = attribute switch
        {
            charAttributes.Strength => PlayerData.strength.ToString(),
            charAttributes.Dexterity => PlayerData.dexterity.ToString(),
            charAttributes.Condition => PlayerData.condition.ToString(),
            charAttributes.Wisdom => PlayerData.wisdom.ToString(),
            charAttributes.Power => PlayerData.power.ToString(),
            _ => inputToLoad.text
        };
    }

    private void LoadCharBasicInfo(charBasicInfo info)
    {
        inputToLoad.text = info switch
        {
            charBasicInfo.Name => PlayerData.charName + " '" + PlayerData.nickname + "'",
            charBasicInfo.Health => PlayerData.health.ToString(),
            _ => inputToLoad.text
        };
    }

    private void LoadCharSkill(charSkills skill)
    {
        inputToLoad.text = skill switch
        {
            charSkills.Electrics => PlayerData.electrics.ToString(),
            charSkills.History => PlayerData.history.ToString(),
            charSkills.Intimidation => PlayerData.intimidation.ToString(),
            charSkills.Locksmithing => PlayerData.locksmithing.ToString(),
            charSkills.Mechanics => PlayerData.mechanics.ToString(),
            charSkills.Medicine => PlayerData.medicine.ToString(),
            charSkills.Occultism => PlayerData.occultism.ToString(),
            charSkills.Perception => PlayerData.perception.ToString(),
            charSkills.Persuasion => PlayerData.persuasion.ToString(),
            charSkills.Psychology => PlayerData.psychology.ToString(),
            _ => inputToLoad.text
        };
    }

    private void SwitchOnOff()
    {
        var root = transform.root;
        root.gameObject.SetActive(false);
        root.gameObject.SetActive(true);
    }
}
