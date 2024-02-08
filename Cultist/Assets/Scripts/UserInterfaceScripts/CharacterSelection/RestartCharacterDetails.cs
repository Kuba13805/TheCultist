using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartCharacterDetails : MonoBehaviour
{
    [SerializeField] private GameObject detailPanel;

    [SerializeField] private ScrollRect scrollRect;
    private void Start()
    {
        CharacterButtonScript.OnCharacterSelected += RestartPanel;
    }

    private void RestartPanel(PlayableCharacter obj)
    {
        detailPanel.SetActive(false);
        
        detailPanel.SetActive(true);
        
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }

    private void OnDisable()
    {
        CharacterButtonScript.OnCharacterSelected -= RestartPanel;
    }
}
