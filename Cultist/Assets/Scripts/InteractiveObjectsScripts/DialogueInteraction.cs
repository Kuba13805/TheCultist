using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    private DialogueController newDialogueController;
    
    [SerializeField] private TextAsset inkAsset;
    public void InteractWithObject()
    {
        newDialogueController = new DialogueController();
        
        newDialogueController.Initialize(inkAsset, GetComponent<InteractableCharacter>().objectName);
    }
}
