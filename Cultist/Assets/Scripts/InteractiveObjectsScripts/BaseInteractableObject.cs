using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
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
    public virtual void Start()
    {
        interactor = GetComponentInChildren<InteractorScript>();
        player = GameObject.FindWithTag("Player");
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(InputManager.Instance.interactableCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
