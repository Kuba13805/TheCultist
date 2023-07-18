using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] private List<PlayableCharacter> characterList;

    #region Events

    public static event Action<List<PlayableCharacter>> ReturnCharacterList; 

    #endregion

    private void OnEnable()
    {
        LoadCharactersToSelect.CallForCharacterList += ReturnCharactersOnCall;
    }

    private void OnDisable()
    {
        LoadCharactersToSelect.CallForCharacterList -= ReturnCharactersOnCall;
    }

    private void ReturnCharactersOnCall()
    {
        ReturnCharacterList?.Invoke(characterList);
    }
}
