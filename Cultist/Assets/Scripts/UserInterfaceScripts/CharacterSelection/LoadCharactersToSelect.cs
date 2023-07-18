using System;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class LoadCharactersToSelect : MonoBehaviour
{
    [SerializeField] private GameObject characterButton;

    private List<PlayableCharacter> charactersToDisplay;

    #region Events

    public static event Action CallForCharacterList;

    #endregion

    private void Start()
    {
        NewGameManager.ReturnCharacterList += LoadCharacters;
        
        CallForCharacterList?.Invoke();
    }


    private void OnDisable()
    {
        NewGameManager.ReturnCharacterList -= LoadCharacters;
    }
    
    private void LoadCharacters(List<PlayableCharacter> charactersList)
    {
        charactersToDisplay = charactersList;
        
        DisplayCharacters(charactersToDisplay);
    }

    private void DisplayCharacters(List<PlayableCharacter> list)
    {
        foreach (var character in list)
        {
            var characterInstance = Instantiate(characterButton, transform);

            characterInstance.GetComponent<CharacterButtonScript>().characterInfo = character;
        }
    }
}
