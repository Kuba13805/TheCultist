using System;
using DG.Tweening;
using PlayerScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerOverButton?.Invoke(characterInfo);
        
        TransformOnPointerEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TransformOnPointerExit();
    }
    private void TransformOnPointerEnter()
    {
        transform.DOScale(1.15f, 0.35f);
    }
    private void TransformOnPointerExit()
    {
        transform.DOScale(1f, 0.35f);
    }
}
