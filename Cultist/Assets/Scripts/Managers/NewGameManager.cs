using System;
using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] private List<PlayableCharacter> characterList;

    #region Events

    public static event Action<List<PlayableCharacter>> ReturnCharacterList;

    public static event Action<Campaign> OnNewGameStart; 

    #endregion

    private void OnEnable()
    {
        LoadCharactersToSelect.CallForCharacterList += ReturnCharactersOnCall;

        ConfirmCharacterSelection.OnCharacterConfirmedSelection += StartNewGameOnCall;
    }


    private void OnDisable()
    {
        LoadCharactersToSelect.CallForCharacterList -= ReturnCharactersOnCall;
    }

    private void ReturnCharactersOnCall()
    {
        ReturnCharacterList?.Invoke(characterList);
    }
    private void StartNewGameOnCall(PlayableCharacter selectedPlayableCharacter)
    {
        var startingCampaign = selectedPlayableCharacter.characterStartingCampaign;
        
        OnNewGameStart?.Invoke(startingCampaign);
    }
}
