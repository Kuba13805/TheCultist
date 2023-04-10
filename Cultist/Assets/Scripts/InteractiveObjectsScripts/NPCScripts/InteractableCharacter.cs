using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractableCharacter : BaseInteractableObject
{
    public string characterName;
    
    public CharacterClass characterClass;

    public void Start()
    {
        characterName ??= characterClass.className;
    }
}
