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

    public static event Action<PlayableCharacter> OnPointerOverButton;

    #endregion
    private void Start()
    {
        if (characterInfo.characterIcon != null)
        {
            GetComponentInChildren<Image>().sprite = characterInfo.characterIcon;
        }
    }

    public void OnButtonClick()
    {
        OnPointerOverButton?.Invoke(characterInfo);
        
        TransformOnClick();

        OnPointerOverButton += ReturnToState;
    }

    private void ReturnToState(PlayableCharacter obj)
    {
        TransformToNormal();
        
        OnPointerOverButton -= ReturnToState;
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
