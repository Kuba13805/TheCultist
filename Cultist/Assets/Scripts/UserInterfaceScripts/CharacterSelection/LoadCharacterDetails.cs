using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadCharacterDetails : MonoBehaviour
{
    #region properties
    
    [SerializeField] private TextMeshProUGUI characterNameBox;
    
    [SerializeField] private TextMeshProUGUI characterQuoteBox;
    
    [SerializeField] private TextMeshProUGUI characterDescBox;

    [SerializeField] private Image characterPortraitBox;

    [SerializeField] private int statValueToBeat;
    

    [BoxGroup("AbilityDisplay")][SerializeField] private Transform abilitiesBox;
    
    [BoxGroup("AbilityDisplay")][SerializeField] private GameObject abilityPrefab;
    
    
    [BoxGroup("AttributeDisplay")][SerializeField] private Transform attributesBox;
    
    [BoxGroup("AttributeDisplay")][SerializeField] private GameObject attributePrefab;
    
    
    [BoxGroup("SkillDisplay")][SerializeField] private Transform goodAtSkillsBox;
    
    [BoxGroup("SkillDisplay")][SerializeField] private Transform badAtSkillsBox;
    
    [BoxGroup("SkillDisplay")][SerializeField] private GameObject skillPrefab;
    

    [BoxGroup("InventoryDisplay")][SerializeField] private Transform startInventory;
    
    [BoxGroup("InventoryDisplay")][SerializeField] private GameObject itemPrefab;
    
    #endregion

    private void Start()
    {
        HidePanel();
        
        CharacterButtonScript.OnCharacterSelected += LoadCharacter;
    }

    private void ShowPanel()
    {
        const bool condition = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            SwitchOffOn(transform.GetChild(i).gameObject, condition);
        }
    }
    
    private void HidePanel()
    {
        const bool condition = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            SwitchOffOn(transform.GetChild(i).gameObject, condition);
        }
    }
    

    private static void SwitchOffOn(GameObject uiElement, bool boolean)
    {
        uiElement.SetActive(boolean);
    }
    private void LoadCharacter(PlayableCharacter character)
    {
        ShowPanel();
        
        LoadBasicCharacterInfo(character);
        
        LoadExtendedCharacterInfo(character);
        
    }

    private void LoadBasicCharacterInfo(PlayableCharacter character)
    {
        characterNameBox.text = character.charName;

        characterQuoteBox.text = character.charQuote;

        characterDescBox.text = character.charDesc;

        characterPortraitBox.sprite = character.playerPortrait;
    }

    private void LoadExtendedCharacterInfo(PlayableCharacter character)
    {
        DisplayCharacterAbilities(character);
        
        DisplayCharacterAttributes(character);
        
        DisplayCharacterSkills(character);
        
        DisplayCharacterInventory(character);
    }

    private void DisplayCharacterAbilities(PlayableCharacter character)
    {
        DeleteAllTransformChildren(abilitiesBox.gameObject);

        foreach (var ability in character.playerAbilities)
        {
            var newAbility = Instantiate(abilityPrefab, abilitiesBox);

            newAbility.GetComponentInChildren<Image>().sprite = ability.abilityIcon;

            newAbility.GetComponent<ShowStatDesc>().desc = ability.abilityName + " - " + ability.abilityDesc;
        }
    }

    private void DisplayCharacterAttributes(PlayableCharacter character)
    {
        DeleteAllTransformChildren(attributesBox.gameObject);
        
        #region Attributes
        
        DisplayMain(character.dexterity, attributePrefab, attributesBox, badAtSkillsBox);
        
        DisplayMain(character.strength, attributePrefab, attributesBox, badAtSkillsBox);
        
        DisplayMain(character.power, attributePrefab, attributesBox, badAtSkillsBox);
        
        DisplayMain(character.condition, attributePrefab, attributesBox, badAtSkillsBox);
        
        DisplayMain(character.wisdom, attributePrefab, attributesBox, badAtSkillsBox);

        #endregion
    }


    private void DisplayCharacterSkills(PlayableCharacter character)
    {
        DeleteAllTransformChildren(goodAtSkillsBox.gameObject);
        
        DeleteAllTransformChildren(badAtSkillsBox.gameObject);

        #region Skills
        
        DisplayMain(character.perception, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.occultism, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.medicine, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.electrics, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.history, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.persuasion, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.intimidation, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.locksmithing, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.mechanics, skillPrefab, goodAtSkillsBox, badAtSkillsBox);

        DisplayMain(character.acrobatics, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.forensics, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.acting, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.alchemy, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.astrology, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.thievery, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.rangedCombat, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.handToHandCombat, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.etiquette, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.animism, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.empathy, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.demonology, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.stealth, skillPrefab, goodAtSkillsBox, badAtSkillsBox);
        
        DisplayMain(character.necromancy, skillPrefab, goodAtSkillsBox, badAtSkillsBox);

        #endregion
        
    }

    private void DisplayCharacterInventory(PlayableCharacter character)
    {
        DeleteAllTransformChildren(startInventory.gameObject);
        
        foreach (var item in character.characterEquipment)
        {
            var newItem = Instantiate(itemPrefab, startInventory);

            newItem.GetComponentInChildren<Image>().sprite = item.icon;

            newItem.GetComponent<ShowStatDesc>().desc = item.itemName + " - " + item.itemDesc;
        }
        
        foreach (var item in character.playerInventoryItems)
        {
            var newItem = Instantiate(itemPrefab, startInventory);

            newItem.GetComponentInChildren<Image>().sprite = item.icon;
            
            newItem.GetComponent<ShowStatDesc>().desc = item.itemName + " - " + item.itemDesc;
        }
    }

    private void DisplayMain(DerivedStat statToCheck, GameObject instancePrefab, Transform mainBox, Transform drawbackBox)
    {
        if (DetermineMain(statToCheck.statValue))
        {
            var newMain = Instantiate(instancePrefab, mainBox);

            newMain.GetComponentInChildren<Image>().sprite = statToCheck.statIcon;

            newMain.GetComponent<ShowStatDesc>().desc = statToCheck.statDesc;
        }
        else if (DetermineDrawback(statToCheck.statValue))
        {
            var newMain = Instantiate(instancePrefab, drawbackBox);

            newMain.GetComponentInChildren<Image>().sprite = statToCheck.statIcon;
            
            newMain.GetComponent<ShowStatDesc>().desc = statToCheck.statDesc;
        }
    }
    
    private bool DetermineMain(int value)
    {
        return value >= statValueToBeat;
    }

    private static bool DetermineDrawback(int value)
    {
        return value < 0;
    }

    private static void DeleteAllTransformChildren(GameObject transformComponent)
    {
        if (transformComponent.transform.childCount == 0) return;

        for (int i = 0; i < transformComponent.transform.childCount; i++)
        {
            Destroy(transformComponent.transform.GetChild(i).gameObject);
        }
    }
}
