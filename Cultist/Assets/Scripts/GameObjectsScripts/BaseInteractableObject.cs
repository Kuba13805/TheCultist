using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractableObject : MonoBehaviour, IInteractable
{
    public string objectName;
    [SerializeField] private int objectId;
    public InteractorScript interactor;
    public GameObject player;

    public virtual void Interact()
    {

    }
    private void Start()
    {
        interactor = GetComponentInChildren<InteractorScript>();
        player = GameObject.FindWithTag("Player");
    }
}
