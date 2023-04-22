using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class InteractableCharacter : BaseInteractableObject
{
    public CharacterClass characterClass;
    public override void Start()
    {
        base.Start();
        objectName ??= characterClass.className;
    }
    public override void Interact()
    {
        var dialogueComponent = GetComponent<DialogueInteraction>();
        if (dialogueComponent == null) return;
        
        dialogueComponent.InteractWithObject();
    }
}
