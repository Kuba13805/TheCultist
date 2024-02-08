using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using NaughtyAttributes;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour
{
    [SerializeField] [Scene] private string baseUIAndPlayerCharacterScene;
    
    [SerializeField] private List<PlayableCharacter> characterList;

    #region Events

    public static event Action<List<PlayableCharacter>> ReturnCharacterList;

    public static event Action<Campaign> OnNewGameStart;

    public static event Action<PlayableCharacter> PassSelectedCharacterInfo;

    public PlayerData PlayerData;

    #endregion

    private void Start()
    {
        LoadCharactersToSelect.CallForCharacterList += ReturnCharactersOnCall;

        ConfirmCharacterSelection.OnCharacterConfirmedSelection += StartNewGameOnCall;
    }

    private void OnDestroy()
    {
        LoadCharactersToSelect.CallForCharacterList -= ReturnCharactersOnCall;
        
        ConfirmCharacterSelection.OnCharacterConfirmedSelection -= StartNewGameOnCall;
    }

    private void ReturnCharactersOnCall()
    {
        ReturnCharacterList?.Invoke(characterList);
    }
    private void StartNewGameOnCall(PlayableCharacter selectedPlayableCharacter)
    {
        var startingCampaign = selectedPlayableCharacter.characterStartingCampaign;
        
        PassSelectedCharacterInfo?.Invoke(selectedPlayableCharacter);
        Debug.Log("New character selected: " + selectedPlayableCharacter.name);
        
        PlayerData.SwitchStats(selectedPlayableCharacter);
        
        OnNewGameStart?.Invoke(startingCampaign);
        Debug.Log(startingCampaign.campaignName);
        

        var baseScenePlayerUi = new CallLocationChange();
        
        baseScenePlayerUi.ChangeLocation(baseUIAndPlayerCharacterScene, false);
        
        var loadFirstScene = new CallLocationChange();
        
        loadFirstScene.ChangeLocation(startingCampaign.firstSceneToLoad, true);
    }
}
