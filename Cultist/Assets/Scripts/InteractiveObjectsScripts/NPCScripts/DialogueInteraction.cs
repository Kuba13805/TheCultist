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
        dialogueAsset = null;
        foreach (var dialogue in dialogueInteractions)
        {
            if (!dialogue.wasSeen && dialogue.oneTimeDialogue)
            {
                var allRequirementsPassed = true;
                
                foreach (var condition in dialogue.RequiredQuests)
                {
                    var conditionPassed = false;

                    switch (condition.questStatus)
                    {
                        case QuestStatus.Completed:
                        {
                            if (condition.requiredQuest.questCompleted)
                            {
                                conditionPassed = true;
                            }

                            break;
                        }
                        case QuestStatus.Started:
                        {
                            if (condition.requiredQuest.questStarted)
                            {
                                conditionPassed = true;
                            }

                            break;
                        }
                        case QuestStatus.NotStarted:
                        {
                            if (!condition.requiredQuest.questStarted)
                            {
                                conditionPassed = true;
                            }

                            break;
                        }
                        default:
                            conditionPassed = false;
                            
                            break;
                    }

                    if (!conditionPassed)
                    {
                        allRequirementsPassed = false;
                    }
                }
                
                // foreach (var dialogueRequirement in dialogue.RequiredQuests)
                // {
                //     switch (dialogueRequirement.questStatus)
                //     {
                //         case QuestStatus.Completed when dialogueRequirement.requiredQuest.questCompleted:
                //             allRequirementsPassed = true;
                //             Debug.Log(dialogueRequirement.requiredQuest.name + ":completed");
                //             break;
                //         case QuestStatus.Started when dialogueRequirement.requiredQuest.questStarted:
                //             Debug.Log(dialogueRequirement.requiredQuest.name + ":started");
                //             allRequirementsPassed = true;
                //             break;
                //         case QuestStatus.NotStarted when !dialogueRequirement.requiredQuest.questStarted:
                //             Debug.Log(dialogueRequirement.requiredQuest.name + ":notStarted");
                //             allRequirementsPassed = true;
                //             break;
                //         default:
                //             allRequirementsPassed = false;
                //             break;
                //     }
                // }
                if (!allRequirementsPassed)
                {
                    continue;
                }

                dialogue.wasSeen = true;
                dialogueAsset = dialogue.dialogueInstance;
                Debug.Log("Current dialog file: " + dialogue.dialogueInstance.name);
                return;
            }

            if (!dialogue.oneTimeDialogue)
            {
                dialogueAsset = dialogue.dialogueInstance;
            }
        }
    }

    private void DeleteConversation()
    {
        Destroy(this);
    }

    private static GameObject LoadDialogueWindowPrefab()
    {
        var prefab = Resources.Load<GameObject>("DialoguePanelNew");
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
