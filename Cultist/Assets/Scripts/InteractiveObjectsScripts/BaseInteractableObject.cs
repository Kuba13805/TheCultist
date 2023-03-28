using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractableObject : MonoBehaviour, IInteractable
{
    public string objectName;
    [SerializeField] public int objectId;
    [HideInInspector] public InteractorScript interactor;
    [HideInInspector] public GameObject player;

    public virtual void Interact()
    {

    }
    private void Start()
    {
        interactor = GetComponentInChildren<InteractorScript>();
        player = GameObject.FindWithTag("Player");
    }
}
