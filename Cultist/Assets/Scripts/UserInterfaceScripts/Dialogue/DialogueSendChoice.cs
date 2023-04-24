using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueSendChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Choice choice;
    private Button _button;
    [SerializeField] private Color normalTextColor;
    [SerializeField] private Color highlightedTextColor;

    public static event Action<Choice> OnChoiceSubmitted;
    
    private void Start()
    {
        _button = GetComponent<Button>();
        GetComponentInChildren<TextMeshProUGUI>().color = normalTextColor;
        
        _button.onClick.AddListener(SubmitChoice);
    }

    private void SubmitChoice()
    {
        OnChoiceSubmitted?.Invoke(choice);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = highlightedTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().color = normalTextColor;
    }
}
