using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractableCharacter : BaseInteractableObject
{

    public CharacterClass characterClass;

    [SerializeField] private bool questCharacter;
    public bool intimidationResistance;
    public bool persuasionResistance;

    [SerializeField] private int difficultyToReadIntension;
    
    private InteractorScript interactor;

    public void Start()
    {
        if (difficultyToReadIntension == 0)
        {
            difficultyToReadIntension = characterClass.difficultyToReadIntension;
        }
        interactor = GetComponentInChildren<InteractorScript>();
    }
}
