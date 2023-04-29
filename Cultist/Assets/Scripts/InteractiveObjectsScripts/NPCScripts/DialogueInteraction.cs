using System;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    private GameObject _dialogueCanvas;
    private GameObject _dialogueWindowPrefab;

    public bool oneTimeConversation;
    public string dialogueSaved;
    public TextAsset dialogueAsset;

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
        Instantiate(_dialogueWindowPrefab, _dialogueCanvas.transform);
        
        OnDialogueCall?.Invoke(GetComponent<InteractableCharacter>().objectName, this);
        
        if (oneTimeConversation)
        {
            DeleteConversation();
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
            canvas = GameObject.Find("DialogueCanvas");
        }
        catch (Exception e)
        {
            Console.WriteLine(e + "Canvas cannot be found");
            throw;
        }
        return canvas;
    }
}
