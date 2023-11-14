using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmCharacterSelection : MonoBehaviour
{
    private PlayableCharacter _selectedCharacter;
    
    public static event Action<PlayableCharacter> OnCharacterConfirmedSelection;
    private void Start()
    {
        CharacterButtonScript.OnCharacterSelected += MakeInteractable;
    }

    private void MakeInteractable(PlayableCharacter character)
    {
        GetComponent<Button>().interactable = true;

        _selectedCharacter = character;
    }

    public void TransferCharacterInfoIntoPlayerData()
    {
        SendSelectedCharacterData(_selectedCharacter.name);
        
        OnCharacterConfirmedSelection?.Invoke(_selectedCharacter);
    }

    private void OnDisable()
    {
        CharacterButtonScript.OnCharacterSelected -= MakeInteractable;
    }

    private static void SendSelectedCharacterData(string fileName)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "characterFileName", fileName },
        };

        AnalyticsService.Instance.CustomData("characterSelected", parameters);
    }
}
