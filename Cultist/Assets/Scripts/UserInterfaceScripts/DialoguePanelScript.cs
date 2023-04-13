using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class DialoguePanelScript : MonoBehaviour
{
    public static event Action OnDialogueShown;

    public static event Action OnDialogueClosed;

    private void OnEnable()
    {
        OnDialogueShown.Invoke();
        GameManager.Instance.PauseGame();
    }

    private void OnDestroy()
    {
        OnDialogueClosed.Invoke();
    }
}
