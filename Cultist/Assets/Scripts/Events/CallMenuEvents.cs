using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallMenuEvents : MonoBehaviour
{
    public static event Action OnMenuShown;

    public static event Action OnMenuClosed;

    private void OnEnable()
    {
        OnMenuShown?.Invoke();
    }

    private void OnDisable()
    {
        OnMenuClosed?.Invoke();
    }
}
