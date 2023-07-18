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
    

    [SerializeField] private Transform abilitiesBox;
    
    [SerializeField] private GameObject abilityPrefab;
    
    
    [SerializeField] private Transform attributesBox;
    
    [SerializeField] private GameObject attributePrefab;
    
    
    [SerializeField] private Transform goodAtSkillsBox;
    
    [SerializeField] private Transform badAtSkillsBox;
    
    [SerializeField] private GameObject skillPrefab;
    

    [SerializeField] private Transform startInventory;
    
    [SerializeField] private GameObject itemPrefab;
    
    #endregion

    private void Start()
    {
        CharacterButtonScript.OnPointerOverButton += LoadCharacter;
    }

    private void LoadCharacter(PlayableCharacter character)
    {
        LoadBasicCharacterInfo(character);
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
        
    }

    private void DisplayCharacterAbilities(PlayableCharacter character)
    {
        DeleteAllTransformChildren(abilitiesBox);

        foreach (var ability in character.playerAbilities)
        {
            var newAbility = Instantiate(abilityPrefab, abilitiesBox);

            newAbility.GetComponentInChildren<Image>().sprite = ability.abilityIcon;
        }
    }

    private void DisplayCharacterAttributes(PlayableCharacter character)
    {
        DeleteAllTransformChildren(attributesBox);

        
    }

    private bool DetermineMain(int value, int valueToBeat)
    {
        return value >= valueToBeat;
    }

    private void DisplayCharacterSkills(PlayableCharacter character)
    {

    }

    private void DisplayCharacterInventory(PlayableCharacter character)
    {
        
    }

    private static void DeleteAllTransformChildren(Component transformComponent)
    {
        foreach (var child in transformComponent.GetComponentsInChildren<Transform>())
        {
            Destroy(child);
        }
    }
}
