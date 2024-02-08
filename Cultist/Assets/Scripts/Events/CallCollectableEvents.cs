using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCollectableEvents : MonoBehaviour
{
    public static event Action OnCollectableShown;
    
    public static event Action OnCollectableClosed;
    private void Start()
    {
        OnCollectableShown?.Invoke();
    }

    private void OnDestroy()
    {
        OnCollectableClosed?.Invoke();
    }
}
