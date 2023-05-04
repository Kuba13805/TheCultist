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
    public enum CharAttributes
    {
        Strength,
        Dexterity,
        Power,
        Wisdom,
        Condition
    }

    public enum CharBasicInfo
    {
        Name,
        Health
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
        Psychology
    }

    private void OnEnable()
    {
        InventoryItemDragDrop.OnItemAddedToInventory += SwitchOnOff;
        
        InventoryItemDragDrop.OnItemRemovedFromInventory += SwitchOnOff;
        
        InventoryItemDragDrop.OnItemEquipped += SwitchOnOff;
        
        InventoryItemDragDrop.OnItemStriped += SwitchOnOff;
        
        _inputToLoad = GetComponent<TextMeshProUGUI>();
        _playerData = GameManager.Instance.playerData;
        LoadData();
        
    }

    private void OnDisable()
    {
        InventoryItemDragDrop.OnItemAddedToInventory -= SwitchOnOff;
        
        InventoryItemDragDrop.OnItemRemovedFromInventory -= SwitchOnOff;
        
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
            CharAttributes.Strength => _playerData.strength.ToString(),
            CharAttributes.Dexterity => _playerData.dexterity.ToString(),
            CharAttributes.Condition => _playerData.condition.ToString(),
            CharAttributes.Wisdom => _playerData.wisdom.ToString(),
            CharAttributes.Power => _playerData.power.ToString(),
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
            CharSkills.Electrics => _playerData.electrics.ToString(),
            CharSkills.History => _playerData.history.ToString(),
            CharSkills.Intimidation => _playerData.intimidation.ToString(),
            CharSkills.Locksmithing => _playerData.locksmithing.ToString(),
            CharSkills.Mechanics => _playerData.mechanics.ToString(),
            CharSkills.Medicine => _playerData.medicine.ToString(),
            CharSkills.Occultism => _playerData.occultism.ToString(),
            CharSkills.Perception => _playerData.perception.ToString(),
            CharSkills.Persuasion => _playerData.persuasion.ToString(),
            CharSkills.Psychology => _playerData.psychology.ToString(),
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
