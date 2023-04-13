using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelScript : MonoBehaviour
{
    [SerializeField] private GameObject PortraitNpc;
    [SerializeField] private GameObject DialogueNpc;
    [SerializeField] private GameObject PlayerChoices;
    private CharacterClass ClassNpc;
    
    public static event Action OnDialogueShown;

    public static event Action OnDialogueClosed;

    private void Start()
    {
        OnDialogueShown?.Invoke();
        GameManager.Instance.PauseGame();
    }

    private void OnDestroy()
    {
        OnDialogueClosed?.Invoke();
    }

    public void Initialize(Sprite portraitToLoad, string nameToLoad, CharacterClass npcCharacterClass)
    {
        if (portraitToLoad == null)
        {
            PortraitNpc.GetComponent<Image>().sprite = npcCharacterClass.classPortrait;
        }
        else
        {
            PortraitNpc.GetComponent<Image>().sprite = portraitToLoad;
        }
    }
}
