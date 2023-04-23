using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolbarVisibility : MonoBehaviour
{
    private Transform[] listOfUiElements;
    private void Start()
    {
        listOfUiElements = GetComponentsInChildren<Transform>();
        
        DialogueInteraction.OnDialogueShown += DialoguePanelScriptOnOnDialogueShown;

        DialogueInteraction.OnDialogueClosed += DialoguePanelScriptOnOnDialogueClosed;
    }

    private void OnDestroy()
    {
        DialogueInteraction.OnDialogueShown -= DialoguePanelScriptOnOnDialogueShown;
        
        DialogueInteraction.OnDialogueClosed -= DialoguePanelScriptOnOnDialogueClosed;
    }

    private void DialoguePanelScriptOnOnDialogueShown()
    {
        foreach (var uiElement in listOfUiElements)
        {
            uiElement.gameObject.SetActive(false);
        }
    }
    private void DialoguePanelScriptOnOnDialogueClosed()
    {
        foreach (var uiElement in listOfUiElements)
        {
            uiElement.gameObject.SetActive(true);
        }
    }
}
