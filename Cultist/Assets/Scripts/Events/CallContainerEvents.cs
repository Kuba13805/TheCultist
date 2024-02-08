using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallContainerEvents : MonoBehaviour
{
    public static event Action OnContainerOpen;
    
    public static event Action OnContainerClosed;

    private void Start()
    {
        OnContainerOpen?.Invoke();
    }

    private void OnDestroy()
    {
        OnContainerClosed?.Invoke();
    }
}
