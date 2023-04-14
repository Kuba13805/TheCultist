using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowHideMainUI : MonoBehaviour
{
    private Transform[] listOfUiElements;
    private void Start()
    {
        listOfUiElements = GetComponentsInChildren<Transform>();
        
        DialoguePanelScript.OnDialogueShown += DialoguePanelScriptOnOnDialogueShown;
        
        DialoguePanelScript.OnDialogueClosed += DialoguePanelScriptOnOnDialogueClosed;
    }

    private void OnDestroy()
    {
        DialoguePanelScript.OnDialogueShown -= DialoguePanelScriptOnOnDialogueShown;
        
        DialoguePanelScript.OnDialogueClosed -= DialoguePanelScriptOnOnDialogueClosed;
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
