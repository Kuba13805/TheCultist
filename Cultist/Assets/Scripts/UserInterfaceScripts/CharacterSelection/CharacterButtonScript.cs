using System;
using DG.Tweening;
using PlayerScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButtonScript : MonoBehaviour
{
    public PlayableCharacter characterInfo;

    #region Events

    public static event Action<PlayableCharacter> OnCharacterSelected;

    #endregion
    private void Start()
    {
        TransformToNormal();
        
        if (characterInfo.characterIcon != null)
        {
            GetComponentInChildren<Image>().sprite = characterInfo.characterIcon;
        }
    }

    private void OnDisable()
    {
        OnCharacterSelected -= ReturnToState;
    }

    public void OnButtonClick()
    {
        OnCharacterSelected?.Invoke(characterInfo);
        
        TransformOnClick();

        OnCharacterSelected += ReturnToState;
    }

    private void ReturnToState(PlayableCharacter obj)
    {
        TransformToNormal();
        
        OnCharacterSelected -= ReturnToState;
    }
    private void TransformOnClick()
    {
        transform.DOScale(1.15f, 0.35f);
    }
    private void TransformToNormal()
    {
        transform.DOScale(1f, 0.35f);
    }
}
