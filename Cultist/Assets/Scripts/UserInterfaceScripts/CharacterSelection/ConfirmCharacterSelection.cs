using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmCharacterSelection : MonoBehaviour
{
    private PlayableCharacter selectedCharacter;
    
    public static event Action<PlayableCharacter> OnCharacterConfirmedSelection;
    private void Start()
    {
        CharacterButtonScript.OnCharacterSelected += MakeInteractable;
    }

    private void MakeInteractable(PlayableCharacter character)
    {
        GetComponent<Button>().interactable = true;

        selectedCharacter = character;
    }

    public void TransferCharacterInfoIntoPlayerData()
    {
        OnCharacterConfirmedSelection?.Invoke(selectedCharacter);
    }
}
