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
        
        DialogueController.OnDialogueShown += HideToolbar;

        DialogueController.OnDialogueClosed += ShowToolbar;

        CallContainerEvents.OnContainerOpen += HideToolbar;
        
        CallContainerEvents.OnContainerClosed += ShowToolbar;
        
        CallCollectableEvents.OnCollectableShown += HideToolbar;
        
        CallCollectableEvents.OnCollectableClosed += ShowToolbar;
    }

    private void OnDestroy()
    {
        DialogueController.OnDialogueShown -= HideToolbar;
        
        DialogueController.OnDialogueClosed -= ShowToolbar;
        
        CallContainerEvents.OnContainerOpen -= HideToolbar;
        
        CallContainerEvents.OnContainerClosed -= ShowToolbar;
        
        CallCollectableEvents.OnCollectableShown -= HideToolbar;
        
        CallCollectableEvents.OnCollectableClosed -= ShowToolbar;
    }

    private void HideToolbar()
    {
        foreach (var uiElement in listOfUiElements)
        {
            uiElement.gameObject.SetActive(false);
        }
    }
    private void ShowToolbar()
    {
        foreach (var uiElement in listOfUiElements)
        {
            uiElement.gameObject.SetActive(true);
        }
    }
}
