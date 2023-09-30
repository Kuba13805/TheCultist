using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class NarrativeEventChoice : MonoBehaviour
{
    private Choice _currentChoice;

    private int _index;

    public static event Action<int> OnChoiceChoosen;

    public void InitializeChoice(Choice choice)
    {
        _currentChoice = choice;
        
        GetComponentInChildren<TextMeshProUGUI>().text = _currentChoice.text;

        _index = _currentChoice.index;
    }

    public void ChooseCurrentChoice()
    {
        OnChoiceChoosen?.Invoke(_index);
    }
    
}
