using Managers;
using NaughtyAttributes;
using PlayerScripts;
using TMPro;
using UnityEngine;

public class LoadCharacterInfo : MonoBehaviour
{
    private TextMeshProUGUI _inputToLoad;
    private PlayerData _playerData;
    [DisableIf(EConditionOperator.Or, "loadBasic", "loadSkill")] 
    public bool loadAttribute;
    [DisableIf(EConditionOperator.Or, "loadAttribute", "loadSkill")] 
    public bool loadBasic;
    [DisableIf(EConditionOperator.Or, "loadBasic", "loadAttribute")] 
    public bool loadSkill;
    [EnableIf("loadAttribute")] [ShowIf("loadAttribute")]
    public CharAttributes attributeToLoad;
    
    [EnableIf("loadBasic")] [ShowIf("loadBasic")]
    public CharBasicInfo basicInfoToLoad;
    
    [EnableIf("loadSkill")] [ShowIf("loadSkill")]
    public CharSkills skillToLoad;

    public enum CharBasicInfo
    {
        Name,
        Health
    }

    private void OnEnable()
    {
        PlayerEvents.OnAddItemToInventory += SwitchOnOff;
        
        PlayerEvents.OnRemoveItemFromInventory += SwitchOnOff;
        
        InventoryItemDragDrop.OnItemEquipped += SwitchOnOff;
        
        InventoryItemDragDrop.OnItemStriped += SwitchOnOff;
        
        _inputToLoad = GetComponent<TextMeshProUGUI>();
        _playerData = GameManager.Instance.playerData;
        LoadData();
        
    }

    private void OnDisable()
    {
        PlayerEvents.OnAddItemToInventory -= SwitchOnOff;
        
        PlayerEvents.OnRemoveItemFromInventory -= SwitchOnOff;
        
        InventoryItemDragDrop.OnItemEquipped -= SwitchOnOff;
        
        InventoryItemDragDrop.OnItemStriped -= SwitchOnOff;
    }

    private void LoadData()
    {
        if (loadAttribute)
        {
            LoadCharAttribute(attributeToLoad);
        }
        else if (loadBasic)
        {
            LoadCharBasicInfo(basicInfoToLoad);
        }
        else if (loadSkill)
        {
            LoadCharSkill(skillToLoad);
        }
    }

    private void LoadCharAttribute(CharAttributes attribute)
    {
        _inputToLoad.text = attribute switch
        {
            CharAttributes.Strength => _playerData.strength.statValue.ToString(),
            CharAttributes.Dexterity => _playerData.dexterity.statValue.ToString(),
            CharAttributes.Condition => _playerData.condition.statValue.ToString(),
            CharAttributes.Wisdom => _playerData.wisdom.statValue.ToString(),
            CharAttributes.Power => _playerData.power.statValue.ToString(),
            _ => _inputToLoad.text
        };
    }

    private void LoadCharBasicInfo(CharBasicInfo info)
    {
        var charNickname = "";

        if (_playerData.nickname != "")
        {
            charNickname = " '" + _playerData.nickname + "'";
        }
        
        _inputToLoad.text = info switch
        {
            CharBasicInfo.Name => _playerData.charName + charNickname,
            CharBasicInfo.Health => _playerData.health.ToString(),
            _ => _inputToLoad.text
        };
    }

    private void LoadCharSkill(CharSkills skill)
    {
        _inputToLoad.text = skill switch
        {
            CharSkills.Electrics => _playerData.electrics.statValue.ToString(),
            CharSkills.History => _playerData.history.statValue.ToString(),
            CharSkills.Intimidation => _playerData.intimidation.statValue.ToString(),
            CharSkills.Locksmithing => _playerData.locksmithing.statValue.ToString(),
            CharSkills.Mechanics => _playerData.mechanics.statValue.ToString(),
            CharSkills.Medicine => _playerData.medicine.statValue.ToString(),
            CharSkills.Occultism => _playerData.occultism.statValue.ToString(),
            CharSkills.Perception => _playerData.perception.statValue.ToString(),
            CharSkills.Persuasion => _playerData.persuasion.statValue.ToString(),
            CharSkills.Acrobatics => _playerData.acrobatics.statValue.ToString(),
            CharSkills.Forensics => _playerData.forensics.statValue.ToString(),
            CharSkills.Acting => _playerData.acting.statValue.ToString(),
            CharSkills.Alchemy => _playerData.alchemy.statValue.ToString(),
            CharSkills.Astrology => _playerData.astrology.statValue.ToString(),
            CharSkills.Thievery => _playerData.thievery.statValue.ToString(),
            CharSkills.RangedCombat => _playerData.rangedCombat.statValue.ToString(),
            CharSkills.HandToHandCombat => _playerData.handToHandCombat.statValue.ToString(),
            CharSkills.Etiquette => _playerData.etiquette.statValue.ToString(),
            CharSkills.Animism => _playerData.animism.statValue.ToString(),
            CharSkills.Empathy => _playerData.empathy.statValue.ToString(),
            CharSkills.Demonology => _playerData.demonology.statValue.ToString(),
            CharSkills.Stealth => _playerData.stealth.statValue.ToString(),
            CharSkills.Necromancy => _playerData.necromancy.statValue.ToString(),
            _ => _inputToLoad.text
        };
    }

    private void SwitchOnOff(BaseItem item)
    {
        var root = transform.root;
        root.gameObject.SetActive(false);
        root.gameObject.SetActive(true);
    }
}
public enum CharAttributes
{
    Strength,
    Dexterity,
    Power,
    Wisdom,
    Condition
}
public enum CharSkills
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
    Acrobatics,
    Forensics,
    Acting,
    Alchemy,
    Astrology,
    Thievery,
    RangedCombat,
    HandToHandCombat,
    Etiquette,
    Animism,
    Empathy,
    Demonology,
    Stealth,
    Necromancy
}
