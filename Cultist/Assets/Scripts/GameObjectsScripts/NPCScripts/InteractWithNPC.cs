using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InteractWithNPC : MonoBehaviour
{
    public string characterName;
    public int characterId;
    
    public NPCClass characterClass;

    public bool questCharacter;
    public bool intimidationResistance;
    public bool persuasionResistance;

    public int difficultyToReadIntension;

    public List<string> contextMenuOptions = new List<string>();
    private InteractorScript interactor;

    public void Start()
    {
        interactor = GetComponentInChildren<InteractorScript>();
    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse is on object");
    }
}
