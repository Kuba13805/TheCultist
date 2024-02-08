using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMenuEvents : MonoBehaviour
{
    public static event Action OnMenuShown;

    public static event Action OnMenuClosed;

    [SerializeField] private GameObject questLogContent; 
    private void OnEnable()
    {
        OnMenuShown?.Invoke();
    }

    private void OnDisable()
    {
        OnMenuClosed?.Invoke();
    }

    private void Start()
    {
        // if (questLogContent.GetComponent<DisplayQuestlogContent>() != null) Destroy(questLogContent.GetComponent<DisplayQuestlogContent>());
        //
        // questLogContent.AddComponent<DisplayQuestlogContent>();
    }
}
