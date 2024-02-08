using UnityEngine;

public class InteractableCharacter : BaseInteractableObject
{
    public CharacterClass characterClass;
    public Sprite characterPortrait;
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
