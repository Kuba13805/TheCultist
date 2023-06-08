using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UserInterfaceScripts.Dialogue;

public class DialogueInteraction : MonoBehaviour
{
    private GameObject _dialogueCanvas;
    private GameObject _dialogueWindowPrefab;

    public bool oneTimeConversation;
    
    [HideInInspector]
    public string dialogueSaved;
    
    public TextAsset dialogueAsset;

    public List<Dialogue> dialogueInteractions;

    #region Events

    public static event Action<string, DialogueInteraction> OnDialogueCall;

    #endregion
    private void Start()
    {
        _dialogueCanvas = GetDialogueCanvas();
        _dialogueWindowPrefab = LoadDialogueWindowPrefab();
    }

    public void InteractWithObject()
    { 
        HandleDialogues();
        
        Instantiate(_dialogueWindowPrefab, _dialogueCanvas.transform);
        
        OnDialogueCall?.Invoke(GetComponent<InteractableCharacter>().objectName, this);
        
        if (oneTimeConversation)
        {
            DeleteConversation();
        }
    }

    private void HandleDialogues()
    {
        foreach (var dialogue in dialogueInteractions)
        {
            if (!dialogue.wasSeen && dialogue.oneTimeDialogue)
            {
                var allRequirementsPassed = true;

                if (dialogue.RequiredQuests == null)
                {
                    dialogue.wasSeen = true;
                    dialogueAsset = dialogue.dialogueInstance;
                    return;
                }
                
                foreach (var dialogueRequirement in dialogue.RequiredQuests)
                {
                    switch (dialogueRequirement.questStatus)
                    {
                        case QuestStatus.Completed when dialogueRequirement.requiredQuest.questCompleted:
                            allRequirementsPassed = true;
                            break;
                        case QuestStatus.Started when dialogueRequirement.requiredQuest.questStarted:
                            allRequirementsPassed = true;
                            break;
                        case QuestStatus.NotStarted when !dialogueRequirement.requiredQuest.questStarted:
                            allRequirementsPassed = true;
                            break;
                        default:
                            allRequirementsPassed = false;
                            break;
                    }
                }

                if (!allRequirementsPassed) continue;
                
                dialogue.wasSeen = true;
                dialogueAsset = dialogue.dialogueInstance;
                return;
            }

            if (dialogue.oneTimeDialogue) continue;
            
            dialogueAsset = dialogue.dialogueInstance;
            return;
        }
    }

    private void DeleteConversation()
    {
        Destroy(this);
    }

    private static GameObject LoadDialogueWindowPrefab()
    {
        var prefab = Resources.Load<GameObject>("DialoguePanel");
        return prefab;
    }
    private static GameObject GetDialogueCanvas()
    {
        GameObject canvas;
        try
        {
            canvas = GameObject.Find("PopupWindowsCanvas");
        }
        catch (Exception e)
        {
            Console.WriteLine(e + "Canvas cannot be found");
            throw;
        }
        return canvas;
    }
}
