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
        Perceptivity,
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
        PlayerData = GameManager.Instance.PlayerData;
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

        if (LoadBasic)
        {
            LoadCharBasicInfo(basicInfoToLoad);
        }

        if (LoadSkill)
        {
            LoadCharSkill(skillToLoad);
        }
    }

    private void LoadCharAttribute(charAttributes attribute)
    {
        switch (attribute)
        {
            case charAttributes.Strength:
                inputToLoad.text = PlayerData.strength.ToString();
                break;
            case charAttributes.Dexterity:
                inputToLoad.text = PlayerData.dexterity.ToString();
                break;
            case charAttributes.Condition:
                inputToLoad.text = PlayerData.condition.ToString();
                break;
            case charAttributes.Wisdom:
                inputToLoad.text = PlayerData.wisdom.ToString();
                break;
            case charAttributes.Power:
                inputToLoad.text = PlayerData.power.ToString();
                break;
        }
    }

    private void LoadCharBasicInfo(charBasicInfo info)
    {
        switch (info)
        {
            case charBasicInfo.Name:
                inputToLoad.text = PlayerData.charName + " '" + PlayerData.nickname + "'";
                break;
            case charBasicInfo.Health:
                inputToLoad.text = PlayerData.health.ToString();
                break;
        }
    }

    private void LoadCharSkill(charSkills skill)
    {
        switch (skill)
        {
            case charSkills.Electrics:
                inputToLoad.text = PlayerData.electrics.ToString();
                break;
            case charSkills.History:
                inputToLoad.text = PlayerData.history.ToString();
                break;
            case charSkills.Intimidation:
                inputToLoad.text = PlayerData.intimidation.ToString();
                break;
            case charSkills.Locksmithing:
                inputToLoad.text = PlayerData.locksmithing.ToString();
                break;
            case charSkills.Mechanics:
                inputToLoad.text = PlayerData.mechanics.ToString();
                break;
            case charSkills.Medicine:
                inputToLoad.text = PlayerData.medicine.ToString();
                break;
            case charSkills.Occultism:
                inputToLoad.text = PlayerData.occultism.ToString();
                break;
            case charSkills.Perceptivity:
                inputToLoad.text = PlayerData.perceptivity.ToString();
                break;
            case charSkills.Persuasion:
                inputToLoad.text = PlayerData.persuasion.ToString();
                break;
            case charSkills.Psychology:
                inputToLoad.text = PlayerData.psychology.ToString();
                break;
        }
    }

    private void SwitchOnOff()
    {
        var root = transform.root;
        root.gameObject.SetActive(false);
        root.gameObject.SetActive(true);
    }
}
